namespace Application.Core;

public class LocalizationSettings
{
    public string[] SupportedCultures { get; set; } = Array.Empty<string>();
    public string DefaultCulture { get; set; } = "en-US";
}