namespace MainFile
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 1
            //employee1.Fname = "";
            //employee1.Lname = "";

            //Console.WriteLine(employee1.Fname);
            //Console.WriteLine(employee1.Lname);
            //Console.WriteLine("45564654----");



            //Employee employee3 = new Employee("Ibrahim", "Mohamed", 20000, 2025);

            //employee1.PrintEmployee();
            //employee2.PrintEmployee();
            //employee3.PrintEmployee(); 
            #endregion

            int x = 10;
            int y = x;

            Console.WriteLine(x);
            Console.WriteLine(y);
            
            y = 2000;

            Console.WriteLine(x);
            Console.WriteLine(y);

            Console.WriteLine("-------------");
            Employee employee1 = new Employee("ahmed","ali2",10000, 2020);
            Employee employee2 = employee1;

            Console.WriteLine(employee1.Fname); // ahmed
            Console.WriteLine(employee2.Fname); // ahmed

            employee2.Fname = "new name form Code";

            Console.WriteLine(employee1.Fname); // ahmed
            Console.WriteLine(employee2.Fname); // new name form Code

        }
    }

    // public - private

    class Employee
    {
        public string Fname { get; set; }
        public string Lname { get; set; }

        public decimal Salary { get; set; }
        public int HireYear { get; set; }

       
        public Employee(decimal salary, int hireYear)
        {
            Salary = salary;
            HireYear = hireYear;
        }

        public Employee(string fname, string lname, decimal salary, int hireYear)
        {
            Fname = fname;
            Lname = lname;
            Salary = salary;
            HireYear = hireYear;
        }

        private decimal CalculateBounse()
        {
            return Salary * 0.10m;
        }

        private int GetYearsOfExp()
        {
            return 2026 - HireYear;
        }

        public void PrintEmployee()
        {
            Console.WriteLine($"Name: {Fname} {Lname}");
            Console.WriteLine($"Salary: {Salary + CalculateBounse()}");
            Console.WriteLine($"Years of Expierinace: {GetYearsOfExp()}");
            Console.WriteLine("---");
        }
    }
}