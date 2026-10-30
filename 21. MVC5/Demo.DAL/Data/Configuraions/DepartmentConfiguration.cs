namespace Demo.DAL.Data.Configuraions
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        private const int maxLength = 100;
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);

            builder.Property(d => d.Name).IsRequired().HasMaxLength(maxLength);
            builder.Property(d => d.Code).IsRequired().HasMaxLength(maxLength);

        }
    }
}