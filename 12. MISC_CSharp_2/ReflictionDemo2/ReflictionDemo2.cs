using System.Reflection;

namespace ReflictionDemo2
{
    public class ReflictionDemo2
    {
        public static void PrintAssemplyName()
        {
            Console.WriteLine("Entry Assembly: " + Assembly.GetEntryAssembly());
            Console.WriteLine("Calling Assembly: " + Assembly.GetCallingAssembly());
            Console.WriteLine("Executing Assembly: " + Assembly.GetExecutingAssembly());
        }
    }
}
