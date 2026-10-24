namespace Delegates
{
    internal class Program
    {
        delegate int CalculatorDelegate(int a, int b);

        //delegate bool Filter<in T>(T item);


        private static void Main(string[] args)
        {

            //Action<int> action = (x) => Console.WriteLine($"{x}");

            //action(1);
            //action(2);
            //action(3);
            //action(4);

        

            #region Collections

            //int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            //if (arr is IEnumerable objects)
            //{
            //    foreach (object i in objects)
            //    {
            //        Console.WriteLine(i);
            //    }
            //}

            //IEnumerable<int> ints = arr;

            //foreach (int item in ints)
            //{
            //    Console.WriteLine(item);
            //}

            //List<int> list = new List<int>(arr);

            //ICollection<int> collection1 = list;

            //collection1.Add(100);
            //collection1.Add(101);

            //foreach (var item in collection1)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine("--------");

            //int count = collection1.Count;

            //bool exsist = collection1.Contains(1000);

            //collection1.Remove(10);

            //foreach (var item in collection1)
            //{
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine("--------");

            //collection1.Clear();

            //foreach (var item in collection1)
            //{
            //    Console.WriteLine(item);
            //}

            //IList<int> iList = list;

            //Console.WriteLine(iList[0]);
            //Console.WriteLine(iList[2]);

            //int index = iList.IndexOf(6);

            //Console.WriteLine(index);
            //Console.WriteLine(iList[index]);

            //iList.Insert(index, 1000);

            //Console.WriteLine(iList[index]);

            //iList.RemoveAt(index);

            //Console.WriteLine("");

            //foreach (var item in iList)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine(list.Capacity);
            //Console.WriteLine(list.Count == list.Capacity);

            //list.Add(56);
            //Console.WriteLine(list.Capacity);
            //Console.WriteLine(list.Count == list.Capacity);

            #endregion Collections

            #region CalculatorDelegate
            //int x = 9;
            //int y = 2;

            //CalculatorDelegate dlg = new CalculatorDelegate(Add);

            //CalculatorWithDelegate(x, y, dlg);

            //CalculatorDelegate dlg2 = Subtract;
            //dlg2 += Add;
            //Add -= Add;
            //dlg2 += Mltiply;
            //CalculatorWithDelegate(x, y, dlg2);
            //Console.WriteLine("------");
            //CalculatorWithDelegate(x, y, Mltiply);
            //CalculatorWithDelegate(x, y, Divide);

            ////%
            ////CalculatorWithDelegate(x, y, delegate (int _1stNumber, int _2ndNumber) { return _1stNumber % _2ndNumber; });
            //CalculatorWithDelegate(x, y, (int _1stNumber, int _2ndNumber) => _1stNumber % _2ndNumber); // Lampda exprtion
            //CalculatorWithDelegate(x, y, (p1, p2) => p1 % p2); // Lampda exprtion 
            #endregion

            List<Employee> employees = new List<Employee>();
            List<int> ints = new List<int>();


            for (int i = 0; i < 100; i++)
            {
                ints.Add(i);
                Employee employee = new Employee()
                {
                    Id = i + 1,
                    Name = $"Name {i + 1}",
                    BasicSalary = Random.Shared.Next(1000, 5001),
                    Deductions = Random.Shared.Next(0, 61),
                    Bouns = Random.Shared.Next(0, 1001)
                };

                employees.Add(employee);
            }

            EmployeeSalaryCalculator employeeSalaryCalculator = new EmployeeSalaryCalculator();

            employeeSalaryCalculator.EmployeeSalaryCalcualted += EmployeeSalaryCalculator_EmployeeSalaryCalcualtedLog;
            employeeSalaryCalculator.EmployeeSalaryCalcualted += EmployeeSalaryCalculator_EmployeeSalaryCalcualted;

            employeeSalaryCalculator.ClculateEmployeeSalary(employees, (e) => e.BasicSalary > 2000 && e.Bouns > 100);



            //ClculateEmployeeSalary(employees, e => e.Deductions < 40);

            //Console.WriteLine();
            //FilterIntWithP(ints, x => x > 5 && x < 60);


        }

        private static void EmployeeSalaryCalculator_EmployeeSalaryCalcualted(Employee employee, int salary)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"sms send to {employee.Name}");
            Console.ResetColor();
        }

        private static void EmployeeSalaryCalculator_EmployeeSalaryCalcualtedLog(Employee employee, int salary)
        {
            Console.WriteLine($"{employee.Id} Emplyee {employee.Name} Salary ({employee.BasicSalary}) = {salary}");
        }

        static void FilterIntWithP(List<int> ints, Predicate<int> predicate)
        {
            foreach (var item in ints)
            {
                if (predicate(item))
                    Console.WriteLine(item);
            }
        }

        #region MyRegion
        //static void FilterIntWithFunc(List<int> ints, Func<int,bool> func)
        //{
        //    foreach (var item in ints)
        //    {
        //        if (func(item))
        //            Console.WriteLine(item);
        //    }
        //}

        //static void FilterInt(List<int> ints, Filter<int> predicate)
        //{
        //    foreach (var item in ints)
        //    {
        //        if (predicate(item))
        //            Console.WriteLine(item);
        //    }
        //} 
        #endregion

        

        #region CalculatorDelegate
        //static void CalculatorWithDelegate(int number1, int number2, CalculatorDelegate calculatorDelegate)
        //{
        //    int result = calculatorDelegate(number1, number2);

        //    Console.WriteLine($"Result: {result}");
        //}

        //static void Calculator(int number1, int number2, char op)
        //{
        //    int result = 0;
        //    if (op == '+')
        //        result = Add(number1, number2);
        //    else if (op == '-')
        //        result = Subtract(number1, number2);
        //    else if (op == '*')
        //        result = Mltiply(number1, number2);
        //    else if (op == '/')
        //        result = Divide(number1, number2);
        //    else
        //        throw new ArgumentException("Invalid oprtaion");

        //    Console.WriteLine($"Result: {result}");
        //}

        //private static int Add(int num1, int num2)
        //{
        //    Console.WriteLine("add");
        //    return num1 + num2;
        //}

        //private static int Subtract(int num1, int num2)
        //{
        //    Console.WriteLine("Subtract");

        //    return num1 - num2;
        //}

        //private static int Mltiply(int num1, int num2)
        //{
        //    Console.WriteLine("Mltiply");

        //    return num1 * num2;
        //}

        //private static int Divide(int num1, int num2)
        //{
        //    Console.WriteLine("Divide");

        //    return num1 / num2;
        //} 
        #endregion
    }
}