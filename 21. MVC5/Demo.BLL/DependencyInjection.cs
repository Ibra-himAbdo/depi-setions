using Demo.BLL.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IValidator<Department>, DepartmentValidator>();
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
