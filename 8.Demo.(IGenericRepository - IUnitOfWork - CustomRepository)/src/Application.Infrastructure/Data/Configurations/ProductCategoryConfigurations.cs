namespace Application.Infrastructure;

internal class ProductCategoryConfigurations : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.Property(C => C.Name)
            .IsRequired();
    }
}
