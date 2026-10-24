using Translation.Abstractions;

namespace SpanishTranslation
{
    public class SpanishTranslation : ITranslationProvider
    {
        public string LanguageName { get; private set; } = "Spanish";

        public string LanguageCode { get; private set; } = "es";

        public string Translate(string key)
        {
            if (key == "Welcome")
                return "Bienvenido";

            if (key == "Login")
                return "Iniciar sesión";

            if (key == "Logout")
                return "Cerrar sesión";

            return key;
        }
    }
}
