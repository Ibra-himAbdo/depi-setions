namespace Application.Infrastructure;

internal class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
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
            .HasForeignKey(P => P.BrandId);

        builder.HasOne(P => P.Category)
            .WithMany()
            .HasForeignKey(P => P.CategoryId);
    }
}