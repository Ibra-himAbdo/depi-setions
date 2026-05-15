using Application.Infrastructure;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Application;

public static class DatabaseMigrator
{
    private static List<string> roles = [ApplicationRoles.Admin, ApplicationRoles.User];

    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;
        ApplicationDbContext dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        UserManager<ApplicationUser> userManger = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        RoleManager<IdentityRole> roleManger = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await dbContext.Database.MigrateAsync();

        await SeedRoles(roleManger);
        await SeedAdmin(userManger);
        await SeedProductsAsync(dbContext);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        foreach (string role in roles)
        {
            bool roleExists = await roleManager.RoleExistsAsync(role);

            if (!roleExists)
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    private static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
    {
        const string email = "hellothere2547@gmail.com";
        const string userName = "Admin";
        const string password = "Password@123";

        ApplicationUser? isAdminCreated = await userManager.FindByEmailAsync(email);

        if (isAdminCreated is not null) return;

        ApplicationUser admin = new()
        {
            FullName = "Admin",
            UserName = userName,
            Email = email,
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(admin, password);

        if (result.Succeeded)
        {
            await userManager.AddToRolesAsync(admin, roles);
        }
    }

    private static async Task SeedProductsAsync(ApplicationDbContext dbContext)
    {
        if (await dbContext.Set<Product>().AnyAsync()) return;

        var faker = new Faker();
        var brandNames = new HashSet<string>();
        var brands = new List<ProductBrand>();

        while (brands.Count < 10)
        {
            var name = faker.Company.CompanyName();

            if (brandNames.Add(name))
            {
                brands.Add(new ProductBrand
                {
                    Name = name
                });
            }
        }

        foreach (var brand in brands)
        {
            brand.NameSecondLanguage = await TranslateToArabic(brand.Name!);
            //await Task.Delay(200);
        }

        await dbContext.Set<ProductBrand>().AddRangeAsync(brands);

        var categoryNames = new HashSet<string>();
        var categories = new List<ProductCategory>();

        while (categories.Count < 10)
        {
            var name = faker.Commerce.Categories(1)[0];

            if (categoryNames.Add(name))
            {
                categories.Add(new ProductCategory
                {
                    Name = name
                });
            }
        }

        foreach (var category in categories)
        {
            category.NameSecondLanguage = await TranslateToArabic(category.Name!);
            //await Task.Delay(200);
        }

        await dbContext.Set<ProductCategory>().AddRangeAsync(categories);


        var productNames = new HashSet<string>();
        var products = new List<Product>();

        while (products.Count < 50)
        {
            var name = faker.Commerce.ProductName();

            if (productNames.Add(name))
            {
                products.Add(new Product
                {
                    Name = name,
                    Description = faker.Commerce.ProductDescription(),
                    PictureUrl = faker.Image.PicsumUrl(),
                    Price = faker.Random.Decimal(10, 1000),
                    BrandId = faker.PickRandom(brands).Id,
                    CategoryId = faker.PickRandom(categories).Id
                });
            }
        }

        foreach (var product in products)
        {
            product.NameSecondLanguage = await TranslateToArabic(product.Name!);
            //await Task.Delay(200);
        }

        await dbContext.Set<Product>().AddRangeAsync(products);

        await dbContext.SaveChangesAsync();
    }

    private static readonly HttpClient _httpClient = new HttpClient();

    private static async Task<string> TranslateToArabic(string text)
    {
        return text;
        if (string.IsNullOrWhiteSpace(text))
            return text;

        var url =
            $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl=ar&dt=t&q={Uri.EscapeDataString(text)}";

        try
        {
            var response = await _httpClient.GetStringAsync(url);

            using var doc = System.Text.Json.JsonDocument.Parse(response);

            var sb = new StringBuilder();

            foreach (var item in doc.RootElement[0].EnumerateArray())
            {
                sb.Append(item[0].GetString());
            }

            return sb.ToString();
        }
        catch
        {
            return text; // fallback
        }
    }
}