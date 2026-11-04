namespace Demo.DAL.Data.Configuraions
{
    internal class AppUserConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(user => user.FullName).HasMaxLength(256);
        }
    }
}
