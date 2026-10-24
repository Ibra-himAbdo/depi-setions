using Translation.Abstraction;

namespace ArabicTranslation
{
    public class ArabicProvider : ITranslationProvider
    {
        public string LanguageName { get; } = "Arabic";

        public string LanguageCode { get; } = "ar";

        public string Translate(string key)
        {
            if (key == "Welcome") return "مرحباً";

            if (key == "Login") return "تسجيل الدخول";

            if (key == "Logout") return "تسجيل الخروج";

            return key;
        }
    }
}