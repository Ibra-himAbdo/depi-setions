using System.Reflection;
using System.Xml.Serialization;

namespace Refliction
{
    class Student
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public double GPA { get; set; }

        public void Print()
        {
            Console.WriteLine($"Name:=> {Name} -- Age: {Age} -- Gpa: {GPA}");
        }
    }

    class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }

        public int Subtract(int a, int b)
        {
            return a - b;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Type studnetType = typeof(Student);

            Console.WriteLine(studnetType.FullName);
            Console.WriteLine(studnetType.Namespace);
            Console.WriteLine(studnetType.Name);

            Console.WriteLine("------");

            PropertyInfo[] props = studnetType.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                Console.WriteLine(prop.Name + " : " + prop.PropertyType.Name);
            }

            Console.WriteLine("------");

            Type calculatorType = typeof(Calculator);

            MethodInfo[] methods = calculatorType.GetMethods();

            foreach (var method in methods)
            {
                Console.WriteLine(method.Name + " : " + method.ReturnType);
            }

            Console.WriteLine("------");
            Console.WriteLine("------ CreateInstance ");

            //Student student = new Student();
            //student.Name = "Test";

            Console.ForegroundColor = ConsoleColor.Green;

            object studentObject = Activator.CreateInstance(studnetType);  // == object student = new Student();

            Console.WriteLine(studentObject.GetType().Name);

            Console.WriteLine("GetProperty");

            PropertyInfo nameProperty = studnetType.GetProperty("Name");

            Console.WriteLine("We Found " + nameProperty?.Name + " : " + nameProperty?.PropertyType.Name);

            nameProperty.SetValue(studentObject, "Test");

            Console.WriteLine(nameProperty.GetValue(studentObject));

            PropertyInfo ageProperty = studnetType.GetProperty("Age");
            Console.WriteLine("We Found " + ageProperty?.Name + " : " + ageProperty?.PropertyType.Name);

            ageProperty.SetValue(studentObject, 65);

            Console.WriteLine(ageProperty.GetValue(studentObject));

            Console.WriteLine("--------- MethodInfo");
            Console.ForegroundColor = ConsoleColor.Blue;

            MethodInfo printMethod = studnetType.GetMethod("Print");

            printMethod?.Invoke(studentObject, null);

            Console.WriteLine("----- calculatorType");

            //Calculator calculator = new Calculator();

            //calculator.Add(5, 6);

            object calculatorObject = Activator.CreateInstance(calculatorType);

            MethodInfo addMethodInfo = calculatorType.GetMethod("Add");

            object result = addMethodInfo.Invoke(calculatorObject, new object[] { 5, 6 });

            Console.WriteLine(result);

            MethodInfo subtractMethodInfo = calculatorType.GetMethod("Subtract");

            object result2 = subtractMethodInfo.Invoke(calculatorObject, new object[] { 5, 6 });

            Console.WriteLine(result2);

            Console.WriteLine("-------------- Assembly");
            Console.ForegroundColor = ConsoleColor.Cyan;

            Assembly assembly = Assembly.GetExecutingAssembly();

            Console.WriteLine(assembly.FullName);
            Console.WriteLine(assembly.GetName().FullName);
            Console.WriteLine(assembly.GetName().Name);
            Console.WriteLine();
            Type[] types = assembly.GetTypes();

            foreach (var type in types)
            {
                Console.WriteLine(type.Name);
            }
            Refliction.Lib1.ReflictionClass1.Print();
        }
    }
}
