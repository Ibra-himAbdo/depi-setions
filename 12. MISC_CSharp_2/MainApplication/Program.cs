using System.Reflection;
using System.Transactions;
using Translation.Abstractions;

namespace MainApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string pluginFolderPath = Path.Combine(AppContext.BaseDirectory, "Plugins");

            if (!Directory.Exists(pluginFolderPath))
            {
                Console.WriteLine("Not Found");
                return;
            }

            string[] dllFiles = Directory.GetFiles(pluginFolderPath, "*.dll");

            ITranslationProvider[] translationProviders = new ITranslationProvider[dllFiles.Length];

            int languageIndex = 0;
            foreach (var file in dllFiles)
            {
                //Console.WriteLine(file);

                Assembly assembly = Assembly.LoadFrom(file);

                foreach (var type in assembly.GetTypes())
                {
                    bool isTransationType = typeof(ITranslationProvider)
                        .IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract;

                    if (isTransationType)
                    {
                        //Console.WriteLine(type.Name);

                        if (Activator.CreateInstance(type) is ITranslationProvider translationProvider)
                        {
                            translationProviders[languageIndex] = translationProvider;
                            languageIndex++;
                        }
                    }
                }

                //Console.WriteLine(assembly.FullName);
            }

            for (int i = 0; i < translationProviders.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {translationProviders[i].LanguageName}");
            }

            while (true)
            {
                Console.Write("Choose Lang. : ");
                int choice = int.Parse(Console.ReadLine());

                ITranslationProvider provider = translationProviders[choice - 1];

                Console.WriteLine(provider.Translate("Welcome"));
                Console.WriteLine(provider.Translate("Login"));
                Console.WriteLine(provider.Translate("Logout"));
                Console.WriteLine();
                Console.WriteLine("----------");
            }
        }
    }
}