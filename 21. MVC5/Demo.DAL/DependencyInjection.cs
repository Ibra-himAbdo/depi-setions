using Demo.DAL.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Default") ?? throw new ArgumentNullException("missing Connection String");

            services.AddDbContext<AppDbContext>
                (option => option.UseSqlServer(connectionString));
            return services;
        }
    }
}
