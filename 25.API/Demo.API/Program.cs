using Demo.API.Errors;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


//app.UseStatusCodePagesWithRedirects("/errors/{0}");


app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseAuthorization();

app.MapControllers();

app.Run();