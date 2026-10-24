namespace OOP2;

internal class Program
{
    private static void Main(string[] args)
    {
        #region Resturant

        //// Instance Memeber => Belong to Object
        //// static memebers => Belong to Class

        //Resturant r1 = new Resturant()
        //{
        //    BranchName = "Tanta Branch"

        //};

        //Console.WriteLine("r1.Id: " + r1.Id);

        //Resturant.ResturantName = "Mac";

        //Console.WriteLine(Resturant.ResturantName);

        //Resturant r2 = new Resturant()
        //{
        //    BranchName = "Maadi Branch"

        //};
        //Console.WriteLine("r2.Id: "+ r2.Id);

        //Resturant r3 = new Resturant()
        //{
        //    BranchName = "Nasr City Branch"
        //};
        //Console.WriteLine("r3.Id: " + r3.Id);

        //Resturant.ResturantName = "Mac2";
        //Console.WriteLine(Resturant.ResturantName);

        #endregion Resturant

        #region Calculator

        //Console.WriteLine(Calculator.Add(1, 2));
        //Console.WriteLine(Calculator.Sub(1, 2));
        //Console.WriteLine(Calculator.Mul(1, 2));

        #endregion Calculator

        #region Poly
        //FullTimeEmp => work from Office
        //FreeLancer => work from home

        // compile time ploymorfisim => method overloading
        // runtime ploymorfisim => method override

        //Employee FullTimeEmp = new FullTimeEmployee();
        //Employee FreeLancer = new FreeLancer();


        //FullTimeEmp.Work();
        //FreeLancer.Work();
        #endregion

        #region abstract
        //// What from How

        ////Shop shop1 = new ClothingShop();
        ////Shop shop2 = new ReaperCarShop();

        ////shop1.Open();
        ////shop1.Close();
        ////shop1.Print();

        ////Console.WriteLine("---------");

        ////shop2.Open();
        ////shop2.Close();
        ////shop2.Print();

        //Console.WriteLine("what type of payemt you want to use? ");
        //Console.WriteLine("1. Cash");
        //Console.WriteLine("2. Vodafone Cash");
        //Console.WriteLine("3. InstaPay");

        //int choose = Convert.ToInt32(Console.ReadLine());
        //Payment payment;


        //switch (choose)
        //{

        //    case 1:
        //        payment = new CashPayment();
        //        payment.PaymentMethod(5000);
        //        break;

        //    case 2:
        //        payment = new VodafoneCashPayment();
        //        payment.PaymentMethod(5000);
        //        break;

        //    case 3:
        //        payment = new InstaPayPayment();
        //        payment.PaymentMethod(5000);
        //        break;
        //} 
        #endregion

        // employee => can work()
        // Robot => can work()
        // Robot is an employee??? No
        // مش مهم انت مين بس عايزك تكون ب تشتغل

        IWorker worker1 = new Emplyee();

        IWorker worker2 = new Robot();

        IMove move = new Emplyee();



        move.Moving();
        worker1.Work();
        worker2.Work();

        // Abstract = what are you?
        // interface = what you can do?
    }


    interface IPayment
    {
        void PaymentMethod(decimal valueToPay);
    }

    class VodafoneCashPayment : IPayment
    {
        public void PaymentMethod(decimal valueToPay)
        {
            Console.WriteLine($"Amount {valueToPay} EGP paid using Vodafone Cash.");
        }
    }

    class CashPayment : IPayment
    {
        public void PaymentMethod(decimal valueToPay)
        {
            Console.WriteLine($"Amount {valueToPay} EGP paid using Cash.");
        }
    }

    class InstaPayPayment : IPayment
    {
        public void PaymentMethod(decimal valueToPay)
        {
            Console.WriteLine($"Amount {valueToPay} EGP paid using InstaPay.");
        }
    }


    interface IWorker1
    {

    }
    interface IWorker2
    {

    }
    interface IWorker3
    {

    }



    interface IWorker : IWorker1, IWorker2, IWorker3
    {
        void Work();
    }

    interface IMove
    {
        void Moving();
    }


    class Emplyee : IWorker, IMove
    {
        public void Moving()
        {
            Console.WriteLine("Employee Moving");
        }

        public void Work()
        {
            Console.WriteLine("Employee Working");
        }
    }

    class Robot : IWorker
    {
        public void Work()
        {
            Console.WriteLine("Robot Working");
        }
    }


    #region abstract
    //abstract class Payment
    //{
    //    public abstract void PaymentMethod(decimal valueToPay);
    //}
    //class VodafoneCashPayment : Payment
    //{
    //    public override void PaymentMethod(decimal valueToPay)
    //    {
    //        Console.WriteLine($"Amount {valueToPay} EGP paid using Vodafone Cash.");
    //    }
    //}

    //class CashPayment : Payment
    //{
    //    public override void PaymentMethod(decimal valueToPay)
    //    {
    //        Console.WriteLine($"Amount {valueToPay} EGP paid using Cash.");
    //    }
    //}

    //class InstaPayPayment : Payment
    //{
    //    public override void PaymentMethod(decimal valueToPay)
    //    {
    //        Console.WriteLine($"Amount {valueToPay} EGP paid using InstaPay.");
    //    }
    //}

    //abstract class Shop
    //{
    //    public string Name { get; set; }

    //    public abstract void Open();

    //    public abstract void Close();

    //    public virtual void Print()
    //    {
    //        Console.WriteLine("Print");
    //    }

    //}

    //class ClothingShop : Shop
    //{
    //    public override void Close()
    //    {
    //        Console.WriteLine("Close at 9:00 pm");
    //    }

    //    public override void Open()
    //    {
    //        Console.WriteLine("Open at 9:00 am");
    //    }
    //}

    //class ReaperCarShop : Shop
    //{
    //    public override void Close()
    //    {
    //        Console.WriteLine("Close at 12:00 am");
    //    }

    //    public override void Open()
    //    {
    //        Console.WriteLine("Open at 12:00 pm");
    //    }

    //    public override void Print()
    //    {
    //        Console.WriteLine("Print from ReaperCarShop");
    //    }
    //} 
    #endregion

    #region Poly
    //class Employee
    //{
    //    public string Name { get; set; }

    //    public virtual void Work()
    //    {
    //        Console.WriteLine("Working...");
    //    }
    //}

    //class FullTimeEmployee : Employee
    //{
    //    public override void Work()
    //    {
    //        Console.WriteLine("Work from Office");
    //    }
    //}

    //class FreeLancer : Employee
    //{
    //    public override void Work()
    //    {
    //        Console.WriteLine("Work from Home");
    //    }
    //}
    #endregion

    #region Calculator

    //static class Calculator
    //{
    //    public static int Add(int x, int y)
    //    {
    //        return x + y;
    //    }

    //    public static int Sub(int x, int y)
    //    {
    //        return x - y;
    //    }
    //    public static int Mul(int x, int y)
    //    {
    //        return x * y;
    //    }

    //}

    #endregion Calculator

    #region Resturant

    //class Resturant
    //{
    //    private static int _id = 0;
    //    public int Id { get { return _id; } }
    //    public string BranchName { get; set; }
    //    public static string ResturantName { get; set; }

    //    public Resturant()
    //    {
    //        Console.WriteLine("normal  public Resturant()");
    //        _id++;
    //    }
    //}

    #endregion Resturant

    #region Emp

    //class Employee
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public decimal Salary { get; set; }

    //    public Employee(int id, string Name, decimal salary) : this(id)
    //    {
    //        Console.WriteLine("Called Employee(int id, string name, decimal salary)");

    //        this.Name = Name;
    //        Salary = salary;

    //        Console.WriteLine(Name);
    //    }

    //    public Employee(int id)
    //    {
    //        Console.WriteLine("Called Employee(int id)");
    //        Id = id;
    //    }

    //    public Employee(Employee other)
    //    {
    //        Id = other.Id;
    //        Name = other.Name;
    //        Salary = other.Salary;
    //    }
    //}

    #endregion Emp
}