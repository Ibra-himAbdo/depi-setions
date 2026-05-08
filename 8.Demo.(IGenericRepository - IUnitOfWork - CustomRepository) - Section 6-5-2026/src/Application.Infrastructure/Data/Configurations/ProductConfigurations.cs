namespace Application.Infrastructure;

internal class ProductConfigurations : BaseSettingEntityConfigurations<Product>, IEntityTypeConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(P => P.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(P => P.Description)
            .IsRequired();

        builder.Property(P => P.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(P => P.PictureUrl);

        builder.HasOne(P => P.Brand)
            .WithMany()
            .HasForeignKey(P => P.BrandId)
             .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(P => P.Category)
            .WithMany()
            .HasForeignKey(P => P.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}