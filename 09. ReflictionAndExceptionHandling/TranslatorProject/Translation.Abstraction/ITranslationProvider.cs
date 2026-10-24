namespace Translation.Abstraction
{
    public interface ITranslationProvider
    {
        string LanguageName { get; }
        string LanguageCode { get; }

        string Translate(string key);
    }
}
