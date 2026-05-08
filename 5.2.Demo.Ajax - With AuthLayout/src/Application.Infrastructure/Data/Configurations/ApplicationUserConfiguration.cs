namespace Application.Infrastructure;

internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(e => e.FullName)
            .IsRequired()
            .HasMaxLength(100);
    }
}