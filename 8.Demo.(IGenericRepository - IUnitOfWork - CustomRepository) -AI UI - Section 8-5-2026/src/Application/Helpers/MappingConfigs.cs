namespace Application;

public class MappingConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Product, ProductToReturnDto>()
            .Map(dest => dest.Brand, src => src.Brand!.Name)
            .Map(dest => dest.BrandNameSecondLanguage, src => src.Brand!.NameSecondLanguage)
            .Map(dest => dest.Category, src => src.Category!.Name)
            .Map(dest => dest.CategoryNameSecondLanguage, src => src.Category!.NameSecondLanguage);
    }
}