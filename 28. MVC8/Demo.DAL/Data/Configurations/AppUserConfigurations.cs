namespace Demo.DAL.Data.Configurations
{
    internal class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(user => user.FullName).HasMaxLength(256);

            builder.HasOne(user => user.Department)
                .WithMany()
                .HasForeignKey(user => user.DepartmentId);
        }
    }
}
