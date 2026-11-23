using Demo.API;
using Demo.API.Errors;
using Demo.API.Extenstions;
using Demo.API.Logging;
using Demo.API.Middelwares;
using Demo.API.Security;
using Demo.Core.Abstractions;
using Demo.Core.Entities;
using Demo.Infrastructure.Data;
using Demo.Infrastructure.Presistence;
using Demo.Services.Auth;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.IdentityModel.Tokens;
using OpenAI;
using Scalar.AspNetCore;
using Serilog;
using System.ClientModel;
using System.Net.Http.Headers;
using System.Text;

LoggingConfig.ConfigureLogging();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

TypeAdapterConfig.GlobalSettings.Scan(typeof(MapConfigs).Assembly);

// Add services to the container.
//builder.Services.AddControllers();
// to fix cycling error
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(opt => opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>());

string connectionString = builder.Configuration.GetConnectionString("DefualtConnection")
    ?? throw new ArgumentNullException("GetConnectionString return null");

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
//builder.Services.AddScoped<IGenericRepository<TodoItem>, GenericRepository<TodoItem>>();
// replasment
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        List<string> errors = context.ModelState.Values
        .SelectMany(e => e.Errors)
        .Select(e => e.ErrorMessage)
        .ToList();

        ApiResponse apiResponse = new(StatusCodes.Status400BadRequest, errors);

        return new BadRequestObjectResult(apiResponse);
    };
});

builder.Services.AddHttpClient("ai", options =>
{
    options.BaseAddress = new Uri("https://api.groq.com/openai/v1/");
    options.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "<your api key>");
});

builder.Services.AddChatClient(sp =>
{
    IChatClient chatClient = new OpenAI.Chat.ChatClient(
        builder.Configuration["Ai:Model"],
        new ApiKeyCredential(builder.Configuration["Ai:ApiKey"]),
        new OpenAIClientOptions()
        {
            Endpoint = new Uri(builder.Configuration["Ai:BaseUri"])
        })
        .AsIChatClient()
        .AsBuilder()
        .UseFunctionInvocation(configure: inv =>
        {
            inv.AllowConcurrentInvocation = false;
        })
        .Build();

    return chatClient;
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),

            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

await app.MigrateDatabase();

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionsMiddelware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTheme(ScalarTheme.Kepler);
        options.WithTitle("API Reference");
        options.Authentication = new ScalarAuthenticationOptions
        {
            PreferredSecuritySchemes = [JwtBearerDefaults.AuthenticationScheme]
        };
    });
}

app.UseHttpsRedirection();

// Redirects to the error page using HTTP 302 (changes the browser URL).
// The original request is lost, and a new request is made to /errors/{statusCode}.
//app.UseStatusCodePagesWithRedirects("/errors/{0}");

// Re-executes the request using the same HTTP context (does NOT change the browser URL).
// The original request URL is preserved
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();