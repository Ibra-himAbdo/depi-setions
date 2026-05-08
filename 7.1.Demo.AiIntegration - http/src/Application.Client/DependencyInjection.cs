using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Application.Client;

public static class DependencyInjection
{
    public const string ApplicationEndpoints = nameof(ApplicationEndpoints);
    public const string ApplicationAI = nameof(ApplicationAI);

    public static IServiceCollection AddClientServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
        //services.AddControllersWithViews()
        //    .AddViewLocalization()
        //    .AddDataAnnotationsLocalization(options =>
        //    {
        //        options.DataAnnotationLocalizerProvider = (type, factory) =>
        //            factory.Create(typeof(Resource));
        //    });

        services.AddHttpClient(ApplicationEndpoints, options =>
        {
            options.BaseAddress = new Uri(configuration["BaseAppUri"] ?? throw new InvalidOperationException("BaseAppUri is not configured"));
        });

        services.AddHttpClient(ApplicationAI, options =>
        {
            options.BaseAddress = new Uri(configuration["AI:BaseUri"]!);
            options.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration["AI:API-Key"]);
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