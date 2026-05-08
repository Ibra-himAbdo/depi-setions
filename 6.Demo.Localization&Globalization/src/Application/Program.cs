using Application.Infrastructure;
using Application.Services;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Serilog Configuration

IConfigurationRoot loggerConfiguration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("serilog.json", optional: false, reloadOnChange: true)
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(loggerConfiguration)
    .CreateLogger();

builder.Host.UseSerilog();

#endregion Serilog Configuration

builder.Services.AddControllers();

// Add Localization
builder.Services.AddLocalization();

// for adding the MVC Client Services Collection
builder.Services.AddClientServices(builder.Configuration);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
TypeAdapterConfig.GlobalSettings.Scan(typeof(MappingConfigs).Assembly);

builder.Services.AddInfrastructure(builder.Configuration)
    .AddApplicationServices(builder.Configuration);

builder.Services.AddOpenApi(opt => opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>());

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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            //ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)),
            ClockSkew = TimeSpan.Zero
        };

        opt.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (string.IsNullOrWhiteSpace(context.Token))
                    context.Token = context.Request.Cookies[ApplicationConstants.AuthCookieName];

                return Task.CompletedTask;
            }
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
        options.Authentication = new ScalarAuthenticationOptions
        {
            PreferredSecuritySchemes = [JwtBearerDefaults.AuthenticationScheme]
        };
    });
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");


var supportedCultures = ApplicationConstants.SupportedCultures;
var defaultCulture = supportedCultures[0];

var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(defaultCulture)
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures)
    .AddInitialRequestCultureProvider(new CookieRequestCultureProvider
    {
        CookieName = ApplicationConstants.LanguageCookieName
    });

app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

// For MVC Client
app.AddClientPipeline();

//app.UseHangfireDashboard();

app.UseHangfireDashboard("/Hangfire", new DashboardOptions
{
    Authorization = [new HangfireAuthorizationFilter()]
});

await app.MigrateDatabaseAsync();

app.Run();