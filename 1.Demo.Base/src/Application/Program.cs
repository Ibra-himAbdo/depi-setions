WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// for adding the MVC Client Services Collection
builder.Services.AddClientServices();

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

WebApplication app = builder.Build();


app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Application API Reference");
        options.WithTheme(ScalarTheme.Kepler);
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

// For MVC Client
app.AddClientPipeline();

app.Run();