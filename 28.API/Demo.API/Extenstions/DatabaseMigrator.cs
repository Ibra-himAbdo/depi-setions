using Dome.Core.Entities;
using Dome.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.API.Extenstions
{
    public static class DatabaseMigrator
    {
        private static readonly string[] DefaultRoles = ["Admin", "User"];

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
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                await context.Database.MigrateAsync();

                await SeedCategories(context);
                await SeedRoles(roleManager, logger);
                await SeedDefaultUser(userManager, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                Console.Error.WriteLine(ex);
            }
        }

        private static async Task SeedCategories(AppDbContext context)
        {
            if (await context.Categories.CountAsync() == 0)
            {
                await context.Categories.AddRangeAsync(Categories);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager, ILogger logger)
        {
            foreach (string role in DefaultRoles)
            {
                if (await roleManager.RoleExistsAsync(role)) continue;

                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(role));

                if (!result.Succeeded)
                {
                    logger.LogError("Failed to seed role {Role}: {Errors}", role,
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedDefaultUser(UserManager<AppUser> userManager, ILogger logger)
        {
            const string email = "admin@demo.com";
            const string username = "admin";
            const string password = "Admin@123";

            AppUser? user = await userManager.FindByEmailAsync(email);

            if (user is null)
            {
                user = new AppUser
                {
                    FullName = "Admin",
                    Email = email,
                    UserName = username,
                    EmailConfirmed = true
                };

                IdentityResult result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    logger.LogError("Failed to seed default user: {Errors}",
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }
            }

            IList<string> currentRoles = await userManager.GetRolesAsync(user);
            string[] rolesToAdd = DefaultRoles.Except(currentRoles).ToArray();

            if (rolesToAdd.Length == 0) return;

            IdentityResult addToRolesResult = await userManager.AddToRolesAsync(user, rolesToAdd);

            if (!addToRolesResult.Succeeded)
            {
                logger.LogError("Failed to add roles to default user: {Errors}",
                    string.Join(", ", addToRolesResult.Errors.Select(e => e.Description)));
            }
        }
    }
}