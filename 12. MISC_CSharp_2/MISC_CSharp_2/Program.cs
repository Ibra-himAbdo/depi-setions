using System.Reflection;

namespace MISC_CSharp_2
{
    // 1. Excption handeling 
    // 2. Refliction
    // 3. Genirics
    class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public double GPA { get; set; }

        public void SayHallo()
        {
            Console.WriteLine("Hello Student - {0}", Name);
        }
    }

    class Calculator
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
        public int Sub(int x, int y)
        {
            return x - y;
        }
    }

    internal class Program
    {
        #region Excption handeling
        //static void RegisterStudent(int age)
        //{
        //    if (age < 18)
        //    {
        //        throw new ArgumentException("Age must be 18 or above");
        //    }
        //} 
        #endregion


        static void Main(string[] args)
        {
            #region Excption handeling
            //while (true)
            //{
            //    try
            //    {
            //        Console.Write("plz enter a number 1: ");
            //        if(!int.TryParse(Console.ReadLine(), out int input1))
            //        {
            //            Console.WriteLine("invalid Number");
            //            continue;
            //        }

            //        //int input1 = int.Parse(Console.ReadLine());

            //        Console.Write("plz enter a number 2: ");
            //        if (!int.TryParse(Console.ReadLine(), out int input2))
            //        {
            //            Console.WriteLine("invalid Number");
            //            continue;
            //        }
            //        //int input2 = int.Parse(Console.ReadLine());

            //        if(input2 == 0)
            //        {
            //            Console.WriteLine("Attempted to divide by zero");
            //            continue;
            //        }

            //        int result = input1 / input2;

            //        Console.WriteLine($"({input1} / {input2}) = {result}");
            //    }
            //    catch (FormatException ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        Console.WriteLine(ex.StackTrace);
            //        Console.WriteLine("Invalid Format");
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        Console.WriteLine(ex.StackTrace);
            //    }
            //}

            //try
            //{
            //    // 0  1  2   
            //    int[] numbers = { 1, 2, 3 };

            //    Console.WriteLine(numbers[5]);
            //}
            //catch(IndexOutOfRangeException ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception.Message);

            //}

            //RegisterStudent(-5);

            //BankAccount bankAccount = new BankAccount()
            //{
            //    Balance = 1000
            //};
            //try
            //{
            //    bankAccount.Withdraw(2000);

            //}
            //catch (Exception ex)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine(ex);
            //    Console.ResetColor();
            //} 
            #endregion


            Type studentType = typeof(Student);


            object studnet = Activator.CreateInstance(studentType); // => new Studnet()

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(studnet.GetType().Name); // Student (نوع المخزن ايه)

            PropertyInfo propertyInfo = studentType.GetProperty("Name");

            propertyInfo.SetValue(studnet, "Ahmed");

            Console.WriteLine(propertyInfo.GetValue(studnet));

            MethodInfo methodInfo = studentType.GetMethod("SayHallo");

            methodInfo.Invoke(studnet, null);

            Console.ResetColor();

            Console.WriteLine("---------");

            PropertyInfo[] propertyInfos = studentType.GetProperties();

            foreach (var prop in propertyInfos)
            {
                Console.WriteLine($"{prop.Name} : {prop.PropertyType.Name}");
            }

            Console.WriteLine("-----------");


            Type calcType = typeof(Calculator);

            object calcContainer = Activator.CreateInstance(calcType);

            MethodInfo addMethod = calcType.GetMethod("Add");

            object result = addMethod.Invoke(calcContainer, new object[] { 10, 20 });

            Console.WriteLine(result);
        }

        #region Excption Handeling
        //class InsufficientBlanceExption : Exception
        //{
        //    public InsufficientBlanceExption(string message) : base(message)
        //    {

        //    }
        //}

        //class BankAccount
        //{
        //    public double Balance { get; set; }

        //    public void Withdraw(double amount)
        //    {
        //        if (amount > Balance)
        //        {
        //            throw new InsufficientBlanceExption("Insufficient Blance");
        //        }
        //    }
        //} 
        #endregion
    }
}


