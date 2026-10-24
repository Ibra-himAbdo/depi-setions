using System.Reflection;

namespace MISC_CSharp_2_P2
{
    class Studnet
    {

    }
    struct Calculator
    {

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            //Assembly assembly = Assembly.GetEntryAssembly();
            //Assembly assembly = Assembly.GetCallingAssembly();

            Console.WriteLine(assembly.GetName().Name);
            Console.WriteLine(assembly.GetName().FullName);
            Console.WriteLine(assembly.GetName().CultureName);
            Console.WriteLine(assembly.GetName().Version);

            Console.WriteLine("-------------");

            foreach (var item in assembly.GetTypes())
            {
                Console.WriteLine(item.Name);
            }

            ReflictionDemo1.ReflictionDemo1.PrintAssemplyName();

        }
    }
}
