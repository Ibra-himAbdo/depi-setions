using Demo.BLL.Repositories;
using Demo.BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IDepretmetService, DepretmetService>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IValidator<Department>, DepartmentValidator>();
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
