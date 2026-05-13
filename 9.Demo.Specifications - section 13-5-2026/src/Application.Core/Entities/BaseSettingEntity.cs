namespace Application.Core;

public abstract class BaseSettingEntity : BaseEntity
{
    public string? Name { get; set; } // en
    public string? NameSecondLanguage { get; set; } // ar

    public string? NormalizedName { get; set; } // en
    public string? NormalizedNameSecondLanguage { get; set; } // ar
}