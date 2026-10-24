namespace MISC_CSharp_1
{
    // poly - intrefaces - abstraction - static
    // voirable scops
    // ref & out
    // Enums

    //public enum Gender : byte
    //{
    //    Male,
    //    Female
    //}

    //public enum Color
    //{
    //    Blue,
    //    Red,
    //    Yallow,
    //    Maginta,
    //    Gray
    //}

    #region Flag enums

    //[Flags]
    //public enum WeekDays
    //{
    //    None = 0b_0000_0000, //0,
    //    Saturday = 0b_0000_0001, //1,
    //    Sunday = 0b_0000_0010, //2,
    //    Monday = 0b_0000_0100, //4,
    //    Tuesday = 0b_0000_1000, //8,
    //    Wednesday = 0b_0001_0000, //16,
    //    Thursday = 0b_0010_0000, //32,
    //    Friday = 0b_0100_0000, //64
    //}

    #endregion Flag enums

    internal class Program
    {
        private static int _x = 0;

        private static void Main(string[] args)
        {
            #region review

            //Employee officeEmployee = new OfficeEmployee()
            //{
            //    Name = "office emp",
            //    Salary = 15_000
            //};

            //Employee freeLancer = new FreeLancer()
            //{
            //    Name = "freeLancer emp",
            //    Salary = 15_000
            //};

            //Employee hyprid = new Hyprid()
            //{
            //    Name = "Hyprid emp",
            //    Salary = 15_000
            //};

            //Employee.GetMaxEmployeeCount();

            //Console.WriteLine(officeEmployee.Id);
            //Console.WriteLine(freeLancer.Id);
            //Console.WriteLine(hyprid.Id);

            //IWorker worker = (OfficeEmployee)officeEmployee;
            //worker.Work();
            //officeEmployee.PrintWokingHours();
            //freeLancer.PrintWokingHours();
            //hyprid.PrintWokingHours();

            #endregion review

            #region scope level

            //int _x = 100;
            //_x++;

            //int y = 5;

            //if(y == 5)
            //{
            //    int z = 50;
            //}

            //int z = 120;

            //Console.WriteLine(_x);

            #endregion scope level

            #region out and ref

            //RefResult();
            //OutResult();

            //Console.Write("Enter number: ");
            //if (int.TryParse(Console.ReadLine(), out int input))
            //{
            //    Console.WriteLine("u had enterd : " + input);
            //}
            //else
            //{
            //    Console.WriteLine("invalid number");
            //}

            #endregion out and ref

            #region simple Enum

            //string[] genders = Enum.GetNames(typeof(Color));

            //foreach (string item in genders)
            //{
            //    Console.WriteLine($"{item}: {(int)Enum.Parse(typeof(Color), item)}" );

            //}

            //Console.Write("Enter color Name: ");

            //string input = Console.ReadLine();

            //Console.WriteLine(Enum.Parse(typeof(Color), input, true));

            #endregion simple Enum

            #region Flag enums

            //WeekDays weekend = WeekDays.Friday | WeekDays.Saturday;
            //Console.WriteLine(weekend);
            //Console.WriteLine((WeekDays)0b_0100_0001);

            //Console.WriteLine("---");
            //WeekDays w1 = WeekDays.Friday | WeekDays.Saturday | WeekDays.Wednesday;
            //WeekDays w2 = WeekDays.Thursday | WeekDays.Wednesday;

            //Console.WriteLine(w1 & w2);

            //Console.WriteLine("---");

            //WeekDays w3 = WeekDays.Friday | WeekDays.Saturday;

            //WeekDays w4 = w3 ^ WeekDays.Friday;
            //WeekDays w5 = w4 ^ WeekDays.Friday;

            //Console.WriteLine(w4);
            //Console.WriteLine(w5);

            #endregion Flag enums

            #region Object
            //Employee employee = new Employee()
            //{
            //    Id = 1,
            //    Name = "Foo",
            //};

            ////object
            //// public virual string ToString()
            //// public virual int GetHashCode()
            //// public virual bool Equals(object )
            //// public Type GetType()
            //string v = employee.ToString();
            //Console.WriteLine(v);
            //Console.WriteLine(employee.ToString());
            //Console.WriteLine(employee);
            //Console.WriteLine("------");
            //Console.WriteLine(employee.GetHashCode());

            //Console.WriteLine("------");
            //Console.WriteLine("------");
            //object employee2 = new Employee()
            //{
            //    Id = 2,
            //    Name = "Test 2",
            //};

            //Console.WriteLine(employee2.ToString());

            //Employee employee3 = new Employee()
            //{
            //    Id = 3,
            //    Name = "Test 3",
            //};

            //Employee employee4 = new Employee()
            //{
            //    Id = 3,
            //    Name = "Test 3",
            //};

            //Console.WriteLine(employee4.Equals(employee3));
            //Console.WriteLine(employee4.GetType());
            //Console.WriteLine(typeof(Employee)); 
            #endregion

            Employee employee1 = new Employee()
            {
                Id = 1,
                Name = "Test 1",
            };

            Employee employee2 = new Employee()
            {
                Id = 2,
                Name = "Test 2",
            };

            Employee employee3 = new Employee()
            {
                Id = 3,
                Name = "Test 3",
            };
            Employee employee4 = new Employee()
            {
                Id = 3,
                Name = "Test 3",
            };
            MyList myList = new MyList();
            myList.Add(employee1);
            myList.Add(employee2);
            myList.Add(employee3);
            myList.Add(employee4);

            Console.WriteLine(myList[0]);

            Employee employee5 = new Employee()
            {
                Id = 5,
                Name = "Test 5",
            };

            myList["Test 3"] = employee5;

            Console.WriteLine(myList[2]);
        }

        #region out nad ref

        private static void RefResult()
        {
            bool isSuccessful = true;

            Console.WriteLine($"result: {Divider(10, 10, ref isSuccessful)}");
            Console.WriteLine($"is Successful : {isSuccessful}");
        }

        private static void OutResult()
        {
            bool isSuccessful;

            Console.WriteLine($"result: {DividerOut(10, 10, out isSuccessful)}");
            Console.WriteLine($"is Successful : {isSuccessful}");
        }

        private static double Divider(double number, double dividor, ref bool isSuccessful)
        {
            if (dividor == 0)
            {
                isSuccessful = false;
                Console.WriteLine("cant divide by zero");
                return 0;
            }

            isSuccessful = true;
            return number / dividor;
        }

        private static double DividerOut(double number, double dividor, out bool isSuccessful)
        {
            if (dividor == 0)
            {
                isSuccessful = false;
                Console.WriteLine("cant divide by zero");
                return 0;
            }

            isSuccessful = true;
            return number / dividor;
        }

        #endregion out nad ref

        //static double Divider(double number, double dividor, ref bool isSuccessful)
        //{
        //    if (dividor == 0)
        //    {
        //        isSuccessful = false;
        //        Console.WriteLine("cant divide by zero");
        //        return 0;
        //    }

        //    isSuccessful = true;
        //    return number / dividor;
        //}

        //static void Print()
        //{
        //    Console.WriteLine(y);
        //}
    }

    class MyList
    {
        private Employee[] _employees = new Employee[1];
        private int _count = 0;

        public Employee this[int index]
        {
            get { return _employees[index]; }
            set { _employees[index] = value; }
        }

        public Employee this[string name]
        {
            get
            {
                foreach (var emp in _employees)
                {
                    if (emp.Name == name)
                        return emp;
                }

                return new();
            }
            set
            {
                for (int i = 0; i < _employees.Length; i++)
                {
                    if (_employees[i].Name == name)
                        _employees[i] = value;
                }
            }
        }


        public void Add(Employee employee)
        {
            if (_count == _employees.Length)
            {
                Employee[] newEmployees = new Employee[_employees.Length * 2];

                for (int i = 0; i < _employees.Length; i++)
                {
                    newEmployees[i] = _employees[i];
                }

                _employees = newEmployees;
            }

            _employees[_count] = employee;
            _count++;
        }
    }

    internal class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"id: {Id} - name: {Name}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Employee employee)
            {
                if (employee.Id != Id)
                    return false;
                if (employee.Name != Name)
                    return false;

                return true;
            }

            return false;
        }

    }

    #region review

    //interface IWorker
    //{
    //    void Work();
    //}

    //abstract class Employee
    //{
    //    private static int _id;
    //    public int Id { get; private set; }
    //    public string Name { get; set; }
    //    public double Salary { get; set; }

    //    static Employee()
    //    {
    //        _id = 0;
    //    }

    //    public Employee()
    //    {
    //        _id++;
    //        Id = _id;
    //    }

    //    public static void GetMaxEmployeeCount()
    //    {
    //        Console.WriteLine(_id);
    //    }

    //    public abstract void PrintWokingHours();

    //}

    //class OfficeEmployee : Employee, IWorker
    //{
    //    public override void PrintWokingHours()
    //    {
    //        Console.WriteLine("Woking 8 hours per day");
    //    }

    //    public void Work()
    //    {
    //        Console.WriteLine("Working from office");
    //    }
    //}

    //class FreeLancer : Employee, IWorker
    //{
    //    public override void PrintWokingHours()
    //    {
    //        Console.WriteLine("Woking any hours per day");
    //    }

    //    public void Work()
    //    {
    //        Console.WriteLine("Working from Home");
    //    }
    //}

    //class Hyprid : Employee, IWorker
    //{
    //    public override void PrintWokingHours()
    //    {
    //        Console.WriteLine("Woking 8 hours per day if it in office");
    //        Console.WriteLine("Woking 8 hours or plus per day if it in home");
    //    }

    //    public void Work()
    //    {
    //        Console.WriteLine("Working from Home or office");
    //    }
    //}

    #endregion review
}