namespace Application.Infrastructure;

internal class BaseSettingEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseSettingEntity
{
    private const int MaxNameLength = 1000;

    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(e => e.Name)
            .HasMaxLength(MaxNameLength)
            .IsRequired();

        builder.Property(e => e.NameSecondLanguage)
            .HasMaxLength(MaxNameLength)
            .IsRequired();

        builder.Property(e => e.NormalizedName)
            .HasMaxLength(MaxNameLength)
            .IsRequired();

        builder.Property(e => e.NormalizedNameSecondLanguage)
            .HasMaxLength(MaxNameLength)
            .IsRequired();
    }
}