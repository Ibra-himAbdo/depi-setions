namespace Application;

public class MappingConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterDto, ApplicationUser>();
    }
}