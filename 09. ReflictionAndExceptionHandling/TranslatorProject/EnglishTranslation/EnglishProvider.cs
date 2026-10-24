using Translation.Abstraction;

namespace EnglishTranslation
{
    public class EnglishProvider : ITranslationProvider
    {
        public string LanguageName { get; } = "English";

        public string LanguageCode { get; } = "en";

        public string Translate(string key)
        {
            if (key == "Welcome") return "Welcome";

            if (key == "Login") return "Login";

            if (key == "Logout") return "Logout";

            return key;
        }
    }
}
