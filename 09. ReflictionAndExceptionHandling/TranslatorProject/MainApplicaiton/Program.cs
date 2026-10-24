using System.Reflection;
using System.Text;
using Translation.Abstraction;

namespace MainApplicaiton
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;


            string languageFolderPath = "Plagins";

            if (!Directory.Exists(languageFolderPath))
            {
                Console.WriteLine("not found Plagins folder app stopped");
                return;
            }

            string[] dllFiles = Directory.GetFiles(languageFolderPath, "*.dll");


            int languageIndex = 0;

            ITranslationProvider[] providers = new ITranslationProvider[dllFiles.Length];

            foreach (var dllFile in dllFiles)
            {
                Assembly assembly = Assembly.LoadFrom(dllFile);

                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    bool isTranslationProvider = typeof(ITranslationProvider)
                        .IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract;

                    if (isTranslationProvider)
                    {
                        ITranslationProvider provider =
                            (ITranslationProvider)Activator.CreateInstance(type);

                        providers[languageIndex] = provider;
                        languageIndex++;
                    }
                }
            }

            for (int i = 0; i < languageIndex; i++)
            {
                Console.WriteLine($"{i + 1} - {providers[i].LanguageName}");
            }

            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out int choose))
                {
                    Console.WriteLine("Enter Valid Number");
                    continue;
                }

                if (choose < 1 || choose > languageIndex)
                {
                    Console.WriteLine($"Valid range is from 1 to {languageIndex}.");
                    continue;
                }

                ITranslationProvider provider = providers[(choose - 1)];

                Console.WriteLine(provider.Translate("Welcome"));
                Console.WriteLine(provider.Translate("Login"));
                Console.WriteLine(provider.Translate("Logout"));
            }


        }
    }
}
