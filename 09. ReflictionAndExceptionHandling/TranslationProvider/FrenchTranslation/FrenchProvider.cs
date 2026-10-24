using Translation.Abstraction;

namespace FrenchTranslation
{
    public class FrenchProvider : ITranslationProvider
    {
        public string LanguageName { get; } = "French";

        public string LanguageCode { get; } = "fr";

        public string Translate(string key)
        {
            if (key == "Welcome") return "Bienvenue";

            if (key == "Login") return "Connexion";

            if (key == "Logout") return "Déconnexion";

            return key;
        }
    }
}