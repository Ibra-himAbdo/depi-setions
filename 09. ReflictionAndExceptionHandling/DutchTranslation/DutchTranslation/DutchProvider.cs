using Translation.Abstraction;

namespace DutchTranslation
{
    public class DutchProvider : ITranslationProvider
    {
        public string LanguageName { get; } = "Dutch";

        public string LanguageCode { get; } = "nl";

        public string Translate(string key)
        {
            if (key == "Welcome") return "Welkom";

            if (key == "Login") return "Inloggen";

            if (key == "Logout") return "Uitloggen";

            return key;
        }
    }
}