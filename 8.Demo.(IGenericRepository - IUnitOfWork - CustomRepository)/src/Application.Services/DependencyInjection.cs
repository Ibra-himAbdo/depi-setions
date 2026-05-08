using Hangfire;
using Hangfire.LiteDB;

namespace Application.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddSingleton<IEmailTemplateService, EmailTemplateService>();


        var connectionString = Path.Combine("Hangfire", "Hangfire.db");
        if (!Directory.Exists("Hangfire"))
            Directory.CreateDirectory("Hangfire");

        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseLiteDbStorage(connectionString);
        });

        services.AddHangfireServer(/*x => x.SchedulePollingInterval = TimeSpan.FromMinutes(30)*/);

        return services;
    }
}