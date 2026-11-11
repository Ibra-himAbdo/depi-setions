using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.MVC.Extensions
{
    public static class DatabaseMigrator
    {
        public static async Task MigrateDatebase(this WebApplication application)
        {
            using IServiceScope scope = application.Services.CreateScope();

            AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            try
            {
                await dbContext.Database.MigrateAsync();
                await SeedRolesAsync(roleManager);
                await SeedAdminUserAsync(userManager);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in AppRoles.Roles)
            {
                bool isRoleExists = await roleManager.RoleExistsAsync(role);

                if (!isRoleExists)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task SeedAdminUserAsync(UserManager<AppUser> userManager)
        {
            const string email = "Admin@123";
            const string username = "Admin";
            const string password = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(email);

            if (adminUser is not null) return;

            adminUser = new()
            {
                FullName = "Admin",
                UserName = username,
                Email = email,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(adminUser, password);

            if(result.Succeeded)
            {
                await userManager.AddToRolesAsync(adminUser, AppRoles.Roles);
            }

        }

    }
}
