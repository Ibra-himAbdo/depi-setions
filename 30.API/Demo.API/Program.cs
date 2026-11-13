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
        .SelectMany(e => e.Errors)
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

// Redirects to the error page using HTTP 302 (changes the browser URL).
// The original request is lost, and a new request is made to /errors/{statusCode}.
//app.UseStatusCodePagesWithRedirects("/errors/{0}");

// Re-executes the request using the same HTTP context (does NOT change the browser URL).
// The original request URL is preserved
app.UseStatusCodePagesWithReExecute("/errors/{0}");


app.UseAuthorization();

app.MapControllers();

app.Run();