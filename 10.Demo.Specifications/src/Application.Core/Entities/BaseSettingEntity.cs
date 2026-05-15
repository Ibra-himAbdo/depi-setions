namespace Application.Core;

public abstract class BaseSettingEntity : BaseEntity
{
    public string? Name { get; set; }
    public string? NameSecondLanguage { get; set; }
}