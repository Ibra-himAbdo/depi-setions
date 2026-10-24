using Translation.Abstractions;

namespace DutchTranslation
{
    public class DutchTranslation : ITranslationProvider
    {
        public string LanguageName { get; private set; } = "Dutch";

        public string LanguageCode { get; private set; } = "nl";

        public string Translate(string key)
        {
            if (key == "Welcome")
                return "Welkom";

            if (key == "Login")
                return "Inloggen";

            if (key == "Logout")
                return "Uitloggen";

            return key;
        }
    }
}
