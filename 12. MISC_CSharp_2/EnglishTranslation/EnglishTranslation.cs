using Translation.Abstractions;

namespace EnglishTranslation
{
    public class EnglishTranslation : ITranslationProvider
    {
        public string LanguageName { get; private set; } = "English";

        public string LanguageCode { get; private set; } = "en";

        public string Translate(string key)
        {
            if (key == "Welcome")
                return "Welcome";

            if (key == "Login")
                return "Login";

            if (key == "Logout")
                return "Logout";

            return key;
        }
    }
}
