namespace OOP2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instance Memeber => created object 
            // Static members => Belongs to Class

            //Resturant resturant1 = new Resturant()
            //{
            //    BranchName = "Tanta"
            //};

            //Console.WriteLine(Resturant.Name);

            //Resturant.Name = "sdasdasd";

            //Resturant resturant2 = new Resturant()
            //{
            //    BranchName = "Zakazik"
            //};

            //Resturant resturant3 = new Resturant()
            //{
            //    BranchName = "Maadi"
            //};


            //Console.WriteLine(resturant3.Id);

            //Resturant.PrintCount();
            //Console.WriteLine(Resturant.Name);

            //Console.WriteLine(Calculator.Add(5,5));
            //Console.WriteLine(Calculator.Add(5l,5l));
            //Console.WriteLine(Calculator.Add(5.5,5.5));
            //Console.WriteLine(Calculator.Add(5,5));
            //Console.WriteLine(Calculator.Sub(5,5));

            // fullTimeEmployee works from office
            // freeLancer works from home

            // run time-polymorphism
            //Employee fullTimeEmployee = new FullTimeEmployee();
            //fullTimeEmployee.Work();

            //Employee freeLancer = new FreeLancer();
            //freeLancer.Work();


            // abstract
            //Shop shop = new ClothingShop();

            //shop.Open();
            //shop.Close();

            //Shop reaper = new CarReparingShop();

            //reaper.Open();
            //reaper.Close();

            // emp => can work
            // robot => can work
            // robot is an emp? No

            //interface
            //IWorker3 worker = new Employee();
            //IWorker worker2 = new Robot();

            //worker.Work();
            //worker.Work(5);
            //worker2.Work();

            // interfaces
            //Console.Write("Choose your payment way: ");

            //int choose = Convert.ToInt32(Console.ReadLine());

            //IPayment payment;

            //switch (choose)
            //{
            //    case 1:
            //        payment = new Cash();
            //        payment.PaymentMethod(100);
            //        break;

            //    case 2:
            //        payment = new InstaPay();
            //        payment.PaymentMethod(100);
            //        break;

            //    case 3:
            //        payment = new ApplePay();
            //        payment.PaymentMethod(100);
            //        break;

            //    default:
            //        Console.WriteLine("Invalid payment method.");
            //        break;
            //}


        }
    }

    #region Interfaces

    //interface IPayment
    //{
    //    void PaymentMethod(double amount);
    //}

    //class Cash : IPayment
    //{
    //    public void PaymentMethod(double amount)
    //    {
    //        Console.WriteLine($"Paid {amount} using Cash.");
    //    }
    //}

    //class InstaPay : IPayment
    //{
    //    public void PaymentMethod(double amount)
    //    {
    //        Console.WriteLine($"Paid {amount} using InstaPay.");
    //    }
    //}

    //class ApplePay : IPayment
    //{
    //    public void PaymentMethod(double amount)
    //    {
    //        Console.WriteLine($"Paid {amount} using Apple Pay.");
    //    }
    //}


    //interface IWorker
    //{
    //    void Work();
    //}

    //interface IWorker2 
    //{
    //    void Work(int times);
    //}

    //interface IWorker3 : IWorker2, IWorker
    //{
    //    void Work(double dada);
    //}

    //class Employee : IWorker3
    //{
    //    public void Work()
    //    {
    //        Console.WriteLine("Employee Working");
    //    }

    //    public void Work(int times)
    //    {
    //        for (int i = 0;i < times; i++)
    //            Console.WriteLine("Employee Working");

    //    }

    //    public void Work(double dada)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //class Robot : IWorker, IWorker2
    //{
    //    public void Work()
    //    {
    //        Console.WriteLine("Robot Working");
    //    }

    //    public void Work(int times)
    //    {
    //        throw new NotImplementedException();
    //    }
    //} 
    #endregion

    #region abstract
    //abstract class Shop
    //{
    //    public int Id;
    //    public string Name { get; set; }
    //    public int CrowCount { get; set; }

    //    public abstract void Open();

    //    public abstract void Close();

    //}

    //class ClothingShop : Shop
    //{
    //    public override void Close()
    //    {
    //        Console.WriteLine("Close at 10:00 pm");
    //    }

    //    public override void Open()
    //    {
    //        Console.WriteLine("Open at 9:00 am");
    //    }
    //}

    //class CarReparingShop : Shop
    //{
    //    public override void Close()
    //    {
    //        Console.WriteLine("Close at 12:00 am");
    //    }

    //    public override void Open()
    //    {
    //        Console.WriteLine("Open at 12:00 pm");
    //    }
    //} 
    #endregion

    #region runtime polymorphism
    //class Employee
    //{

    //    public int Id { get; set; }
    //    public decimal Salary { get; set; }
    //    public string Name { get; set; }

    //    public virtual void Work()
    //    {
    //        Console.WriteLine("working...");
    //    }

    //}

    //class FullTimeEmployee : Employee
    //{
    //    public override void Work()
    //    {
    //        Console.WriteLine("working from Office");
    //    }
    //}

    //class FreeLancer : Employee
    //{
    //    public override void Work()
    //    {
    //        Console.WriteLine("working from home");
    //    }
    //} 
    #endregion

    #region Static + cmpile time polymorphism
    //static class Calculator
    //{

    //    #region Combile time polymorphism = method overLoading
    //    public static int Add(int a, int b)
    //    {
    //        return a + b;
    //    }

    //    public static long Add(long a, long b)
    //    {
    //        return a + b;
    //    }

    //    public static double Add(double a, double b)
    //    {
    //        return a + b;
    //    } 
    //    #endregion

    //    public static int Sub(int a, int b)
    //    {
    //        return a - b;
    //    }
    //}

    //class Resturant
    //{
    //    private static int _instanceCount = 0;
    //    public int Id { get; private set; }
    //    public static string Name { get; set; } = "Bazoka";
    //    public string BranchName { get; set; }

    //    static Resturant()
    //    {
    //        Console.ForegroundColor = ConsoleColor.Red;
    //        Console.WriteLine("static ctor called");
    //        Console.ResetColor();
    //    }

    //    public Resturant()
    //    {
    //        _instanceCount++;
    //        Id = _instanceCount;

    //        Console.WriteLine("public ctor called");
    //    }

    //    public static void PrintCount()
    //    {
    //        Console.WriteLine(_instanceCount);
    //    }
    //} 
    #endregion

    #region constractor chaning
    //class BaseEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }

    //    public BaseEntity(int id)
    //    {
    //        Id = id;
    //    }

    //    public BaseEntity(int id, string name = "") : this(id)
    //    {
    //        Name = name;
    //    }
    //}

    //class Employee : BaseEntity
    //{

    //    public decimal Salary { get; set; }

    //    public Employee(int id) : base(id)
    //    {
    //    }

    //    public Employee(int id, string name, decimal salary) : base(id, name)
    //    {
    //        Name = name;
    //        Salary = salary;
    //    }
    //} 
    #endregion
}
