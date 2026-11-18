using Dome.Core.Entities;
using Dome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.API.Extenstions
{
    public static class DatabaseMigrator
    {
        private static readonly Category[] Categories =
        {
            new Category
            {
                Name = "Work",
                Desctription = "Tasks related to work and professional activities."
            },
            new Category
            {
                Name = "Personal",
                Desctription = "Personal tasks and daily activities."
            },
            new Category
            {
                Name = "Shopping",
                Desctription = "Shopping lists and things to purchase."
            }
        };

        public static async Task MigrateDatabase(this WebApplication application)
        {
            using IServiceScope scope = application.Services.CreateScope();
            AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                await context.Database.MigrateAsync();

                await SeedCategories(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Console.Error.WriteLine(ex);
            }
        }

        private static async Task SeedCategories(AppDbContext context)
        {
            if (!await context.Categories.AnyAsync())
            {
                await context.Categories.AddRangeAsync(Categories);
                await context.SaveChangesAsync();
            }
        }
    }
}