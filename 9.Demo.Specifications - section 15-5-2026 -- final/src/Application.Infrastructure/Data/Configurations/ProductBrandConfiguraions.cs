namespace Application.Infrastructure;

internal class ProductBrandConfigurations : BaseSettingEntityConfigurations<ProductBrand>, IEntityTypeConfiguration<ProductBrand>
{
    public override void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        base.Configure(builder);
    }
}