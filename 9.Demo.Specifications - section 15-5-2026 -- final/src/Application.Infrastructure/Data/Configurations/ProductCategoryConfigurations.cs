namespace Application.Infrastructure;

internal class ProductCategoryConfigurations : BaseSettingEntityConfigurations<ProductCategory>, IEntityTypeConfiguration<ProductCategory>
{
    public override void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        base.Configure(builder);
    }
}