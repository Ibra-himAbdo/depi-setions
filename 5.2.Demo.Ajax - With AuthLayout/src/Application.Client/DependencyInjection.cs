using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Client;

public static class DependencyInjection
{
    public const string ApplicationEndpoints = nameof(ApplicationEndpoints);

    public static IServiceCollection AddClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();

        services.AddHttpClient(ApplicationEndpoints, options =>
        {
            options.BaseAddress = new Uri(configuration["BaseAppUri"] ?? throw new InvalidOperationException("BaseAppUri is not configured"));
        });

        return services;
    }

    public static WebApplication AddClientPipeline(this WebApplication app)
    {
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();
        return app;
    }
}