using Application;
using Demo.API;
using Demo.API.Errors;
using Demo.API.Extenstions;
using Demo.API.Logging;
using Demo.API.Middlewares;
using Dome.Core.Abstractions;
using Dome.Core.Entities;
using Dome.Infrastructure.Data;
using Dome.Infrastructure.Presistence;
using Dome.Services.Auth;
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

TypeAdapterConfig.GlobalSettings.Scan(typeof(MapConfigs).Assembly);

builder.Host.UseSerilog();

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

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        List<string> errors = context.ModelState.Values
        .SelectMany(x => x.Errors)
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
    options.Timeout = TimeSpan.FromSeconds(60);
});







string connectionString = builder.Configuration
    .GetConnectionString("Default")
    ?? throw new ArgumentNullException("missing Connection String");

builder.Services.AddDbContext<AppDbContext>
    (option => option.UseSqlServer(connectionString));

builder.Services.AddIdentity<AppUser, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

//builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
//builder.Services.AddScoped<IGenericRepository<TodoItem>, GenericRepository<TodoItem>>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = "",
        ValidAudience = "",

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecritKey"])),

        ClockSkew = TimeSpan.Zero

    };
});


builder.Services.AddChatClient(sp =>
{
    IChatClient chatClient = new OpenAI.Chat.ChatClient(
                builder.Configuration["AI:Model"],
                new ApiKeyCredential(builder.Configuration["AI:Apikey"]),
                new OpenAIClientOptions()
                {
                    Endpoint = new Uri(builder.Configuration["AI:BaseUri"])
                })
                .AsIChatClient()
                .AsBuilder()
                .UseFunctionInvocation(configure: inv => inv.AllowConcurrentInvocation = false)
                .Build();

    return chatClient;
});


var app = builder.Build();

await app.MigrateDatabase();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTheme(ScalarTheme.Kepler);
        options.WithTitle("Api Reference");
        options.Authentication = new ScalarAuthenticationOptions
        {
            PreferredSecuritySchemes = [JwtBearerDefaults.AuthenticationScheme]
        };
    });
}

app.UseHttpsRedirection();


//app.UseStatusCodePagesWithRedirects("/errors/{0}");


app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseAuthorization();

app.MapControllers();

app.Run();