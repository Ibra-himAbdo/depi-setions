namespace Demo.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            //services.AddScoped<IValidator<Department>, DepartmentValidator>();     // if u forget and add ur validation as intrinal class
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

            return services;
        }
    }
}
