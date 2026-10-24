namespace Demo.DAL.Data.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        const int maxLength = 100;
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.Name).HasMaxLength(maxLength);
            builder.Property(d => d.Code).HasMaxLength(maxLength);
        }
    }
}
