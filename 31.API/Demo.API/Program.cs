using Demo.API.Errors;
using Demo.API.Extenstions;
using Demo.API.Logging;
using Demo.API.Middelwares;
using Demo.Core.Abstractions;
using Demo.Infrastructure.Data;
using Demo.Infrastructure.Presistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

LoggingConfig.ConfigureLogging();

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddOpenApi();


string connectionString = builder.Configuration.GetConnectionString("DefualtConnection") 
    ?? throw new ArgumentNullException("GetConnectionString return null");

builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));

//builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
//builder.Services.AddScoped<IGenericRepository<TodoItem>, GenericRepository<TodoItem>>();
// replasment 
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();


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
    });
}

app.UseHttpsRedirection();

// Redirects to the error page using HTTP 302 (changes the browser URL).
// The original request is lost, and a new request is made to /errors/{statusCode}.
//app.UseStatusCodePagesWithRedirects("/errors/{0}");

// Re-executes the request using the same HTTP context (does NOT change the browser URL).
// The original request URL is preserved
app.UseStatusCodePagesWithReExecute("/errors/{0}");


app.UseAuthorization();

app.MapControllers();

app.Run();