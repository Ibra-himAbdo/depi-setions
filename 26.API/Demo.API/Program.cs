using Demo.API.Errors;
using Demo.API.Extenstions;
using Demo.API.Logging;
using Demo.API.Middlewares;
using Dome.Core.Abstractions;
using Dome.Core.Entities;
using Dome.Infrastructure.Data;
using Dome.Infrastructure.Presistence;
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


string connectionString = builder.Configuration
    .GetConnectionString("Default")
    ?? throw new ArgumentNullException("missing Connection String");

builder.Services.AddDbContext<AppDbContext>
    (option => option.UseSqlServer(connectionString));

//builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
//builder.Services.AddScoped<IGenericRepository<TodoItem>, GenericRepository<TodoItem>>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();

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
    });
}

app.UseHttpsRedirection();


//app.UseStatusCodePagesWithRedirects("/errors/{0}");


app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseAuthorization();

app.MapControllers();

app.Run();