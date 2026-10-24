using ConsoleApp15646546654;
using System.Threading.Channels;

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

            #region 2
            //int x = 10;
            //int y = x;

            //Console.WriteLine("X " + x);
            //Console.WriteLine("Y " + y);

            //y = 2000;

            //Console.WriteLine("X " + x);
            //Console.WriteLine("Y " + y);

            //Console.WriteLine("-------------");
            //Employee employee1 = new Employee("ahmed", "ali2", 10000, 2020);
            //Employee employee2 = employee1;

            //Console.WriteLine(employee1.Fname);
            //Console.WriteLine(employee2.Fname);

            //employee2.Fname = "new name form Code";

            //Console.WriteLine(employee1.Fname);
            //Console.WriteLine(employee2.Fname);
            #endregion

            Topic topic = new Topic()
            {
                Id = 1,
                Name = "Test",
                Date = "20-8-2026"
            };

            topic.Print();

            Console.WriteLine("8489--------12564");

            // L in SOLID
            BaseEntity topic2 = new Topic();
            topic2.Id = 2;
            topic2.Name = "Test2";
            Console.WriteLine(topic2.Name);

            Course course = new Course()
            {
                Id = 1,
                Name = "Test Course",
                Dureation = 30,
                Desc = "test test test test test test",
                TopicId = topic.Id,
            };

            course.Print();

            Console.WriteLine($"Course Details:");
            Console.WriteLine($"-------------------------");
            Console.WriteLine($"ID:          {course.Id}");
            Console.WriteLine($"Name:        {course.Name}");
            Console.WriteLine($"Duration:    {course.Dureation} minutes");
            Console.WriteLine($"Description: {course.Desc}");
            Console.WriteLine($"Topic ID:    {course.TopicId}");
            Console.WriteLine($"Topic:       {topic.Name}");
            Console.WriteLine($"CCCCCC:       {topic.EntityName}");
            Console.WriteLine($"-------------------------");



            //EmployeeFormOutSide emp = new();

        }
    }

    // base class , parent , super class
    class BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        internal string EntityName { get; set; }

        public BaseEntity()
        {
            EntityName = nameof(BaseEntity);
        }

        public virtual void Print()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine($"ID:   {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"EntityName: {EntityName}");
            Console.WriteLine("-------------------------");
        }
    }

    class BaseEntityWithCreateDate : BaseEntity
    {
        public string CreateDate { get; set; }

        public BaseEntityWithCreateDate()
        {
            EntityName = nameof(BaseEntityWithCreateDate);
        }
    }


    // derived , child
    class Topic : BaseEntity
    {
        public string Date { get; set; }

        public override void Print()
        {
            Console.WriteLine("Topic Info");
            Console.WriteLine("-------------------------");
            Console.WriteLine($"ID:   {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine("-------------------------");
        }

        public Topic()
        {
            EntityName= nameof(Topic);
        }
    }

    // derived , child
    class Course : BaseEntityWithCreateDate
    {
        public int Dureation { get; set; }
        public string Desc { get; set; }
        public int TopicId { get; set; }

        public Course()
        {
            EntityName = nameof(Course);
        }
    }


    // public - private
    #region eMPLOYEE class
    //class Employee
    //{
    //    public string Fname { get; set; }
    //    public string Lname { get; set; }

    //    public decimal Salary { get; set; }
    //    public int HireYear { get; set; }


    //    public Employee(decimal salary, int hireYear)
    //    {
    //        Salary = salary;
    //        HireYear = hireYear;
    //    }

    //    public Employee(string fname, string lname, decimal salary, int hireYear)
    //    {
    //        Fname = fname;
    //        Lname = lname;
    //        Salary = salary;
    //        HireYear = hireYear;
    //    }

    //    private decimal CalculateBounse()
    //    {
    //        return Salary * 0.10m;
    //    }

    //    private int GetYearsOfExp()
    //    {
    //        return 2026 - HireYear;
    //    }

    //    public void PrintEmployee()
    //    {
    //        Console.WriteLine($"Name: {Fname} {Lname}");
    //        Console.WriteLine($"Salary: {Salary + CalculateBounse()}");
    //        Console.WriteLine($"Years of Expierinace: {GetYearsOfExp()}");
    //        Console.WriteLine("---");
    //    }
    //} 
    #endregion
}