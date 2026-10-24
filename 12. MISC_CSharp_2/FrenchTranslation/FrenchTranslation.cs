using Translation.Abstractions;

namespace FrenchTranslation
{
    public class FrenchTranslation : ITranslationProvider
    {
        public string LanguageName { get; private set; } = "French";

        public string LanguageCode { get; private set; } = "fr";

        public string Translate(string key)
        {
            if (key == "Welcome")
                return "Bienvenue";

            if (key == "Login")
                return "Connexion";

            if (key == "Logout")
                return "Déconnexion";

            return key;
        }
    }
}
