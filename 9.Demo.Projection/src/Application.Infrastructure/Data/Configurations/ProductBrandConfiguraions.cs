namespace Application.Infrastructure;

internal class ProductBrandConfigurations : IEntityTypeConfiguration<ProductBrand>
{
    public void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        builder.Property(B => B.Name)
            .IsRequired();
    }
}