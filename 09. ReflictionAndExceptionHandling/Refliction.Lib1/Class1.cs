using System.Reflection;

namespace Refliction.Lib1
{
    public static class ReflictionClass1
    {
        public static void Print()
        {
            var assembly = Assembly.GetCallingAssembly();
            Console.WriteLine(assembly.GetName().Name);
            Console.WriteLine();
            Console.WriteLine(Assembly.GetEntryAssembly().GetName().Name);
            Console.WriteLine(Assembly.GetExecutingAssembly().GetName().Name);
        }
    }
}
