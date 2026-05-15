using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application;

public class MappingConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        bool isRtl = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;

        config.NewConfig<Product, ProductToReturnDto>()
            .Map(dest => dest.Brand, src => src.Brand!.Name)
            .Map(dest => dest.BrandNameSecondLanguage, src => src.Brand!.NameSecondLanguage)
            .Map(dest => dest.Category, src => src.Category!.Name)
            .Map(dest => dest.CategoryNameSecondLanguage, src => src.Category!.NameSecondLanguage);

        config.NewConfig<ProductBrand, SelectListItem>()
            .Map(dest => dest.Value, src => src.Id.ToString())
            .Map(dest => dest.Text, src => isRtl ? src.NameSecondLanguage : src.Name);
    }
}