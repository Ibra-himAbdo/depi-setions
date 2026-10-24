using System.Xml.Serialization;

namespace Delegates
{

    internal class Program
    {
        private delegate int CalaculatorDelegate(int a, int obj);

        //private delegate bool Filter<in T>(T item);
        //        delegate bool Predicate<in T>(T obj)
        private static void Main(string[] args)
        {
            //Action action = () => Console.WriteLine("DADADADADADA");

            //Action<int, int> action1 = (x, y) => Console.WriteLine(x + y);


            //action();
            //action1(10, 2);


            List<Employee> employees = new List<Employee>();
            List<int> ids = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                Employee employee = new Employee()
                {
                    Name = $"Name {i + 1}",
                    BasicSalary = Random.Shared.Next(1000, 7001),
                    Deductions = Random.Shared.Next(0, 61),
                    Bouns = Random.Shared.Next(0, 1001),
                };
                ids.Add(i);
                employees.Add(employee);
            }

            EmployeeSalaryCalculator employeeSalaryCalculator = new EmployeeSalaryCalculator();

            employeeSalaryCalculator.EmployeeSalaryCalculated += EmployeeSalaryCalculator_EmployeeSalaryCalculated;

            employeeSalaryCalculator.EmployeeSalaryCalculated += EmailHnadler;

            employeeSalaryCalculator.CalculateEmployeeSalary(employees, e => e.BasicSalary < 2000);

            //CalculateEmployeeSalaryWithDelegate(employees, e => e.BasicSalary < 2000 && e.Bouns > 10);
            //ApplyFiltorOnList(employees, e => e.BasicSalary > 6000);
            //ApplyFiltorOnList(ids, x => x < 90 && x > 80);

            //ids.ApplyFiltorOnList(x => x < 90 && x > 80);

            #region dlg1

            //int x = 10;
            //int y = 3;

            //CalaculatorDelegate dlg = new CalaculatorDelegate(Add);

            //CalaculatorWithDelegate(x, y, dlg);

            //CalaculatorDelegate dlg2 = Subtract;

            //dlg2 += Add;
            //dlg2 += Multiply;
            //dlg2 -= Add;

            //CalaculatorWithDelegate(x, y, dlg2);
            //CalaculatorWithDelegate(x, y, Multiply);
            //CalaculatorWithDelegate(x, y, Divide);
            ////CalaculatorWithDelegate(x, y, delegate (int num1, int num2) { return num1 % num2; });
            ////CalaculatorWithDelegate(x, y, (int number, int numberr) => number % numberr); // lampda expretion
            //CalaculatorWithDelegate(x, y, (number, numberr) => number % numberr); // lampda expretion

            // x + 5 * x / y ^ 5
            // %

            #endregion dlg1
        }

        private static void EmailHnadler(Employee employee, int salary)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Email Send To employee {employee.Name}");
            Console.ResetColor();
        }

        private static void EmployeeSalaryCalculator_EmployeeSalaryCalculated(Employee employee, int salary)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"Salary for employee {employee.Name} - ({employee.BasicSalary}) = {salary}");
            Console.ResetColor();
        }

        //private static void ApplyFiltorOnList<T>(List<T> items, Filter<T> predicate)
        //{
        //    foreach (T item in items)
        //    {
        //        if (predicate(item))
        //        {
        //            Console.WriteLine(item);
        //        }
        //    }
        //}

        //static void ApplyFiltorOnList<T>(List<Employee> employees, Filter<Employee> predicate)
        //{
        //    foreach (Employee employee in employees)
        //    {
        //        if (predicate(employee))
        //        {
        //            var salary = employee.BasicSalary - employee.Deductions + employee.Bouns;

        //            Console.WriteLine($"Salary for employee {employee.Name} - ({employee.BasicSalary}) = {salary}");
        //        }

        //    }
        //}

        private static void CalculateEmployeeSalary(List<Employee> employees, int minSalary)
        {
            foreach (Employee employee in employees)
            {
                if (minSalary > employee.BasicSalary)
                {
                    var salary = employee.BasicSalary - employee.Deductions + employee.Bouns;

                    Console.WriteLine($"Salary for employee {employee.Name} - ({employee.BasicSalary}) = {salary}");
                }
            }
        }

        private static void CalculateEmployeeSalary(List<Employee> employees, int minSalary, int maxSalary)
        {
            foreach (Employee employee in employees)
            {
                if (minSalary <= employee.BasicSalary && maxSalary >= employee.BasicSalary)
                {
                    var salary = employee.BasicSalary - employee.Deductions + employee.Bouns;

                    Console.WriteLine($"Salary for employee {employee.Name} - ({employee.BasicSalary}) = {salary}");
                }
            }
        }

        #region dLG 1

        private static void CalaculatorWithDelegate(int num1, int num2, CalaculatorDelegate calaculatorDelegate)
        {
            int result = calaculatorDelegate(num1, num2);

            Console.WriteLine($"Result = {result}");
            Console.WriteLine("----");
        }

        private static void Calaculator(int num1, int num2, char op)
        {
            int result = 0;
            if (op == '+')
                result = Add(num1, num2);
            else if (op == '-')
                result = Subtract(num1, num2);
            else if (op == '*')
                result = Multiply(num1, num2);
            else if (op == '/')
                result = Divide(num1, num2);
            //else if (op == '%')
            //    result = Remaing(num1, num2);
            else
                throw new ArgumentException("Invalid Opration");

            Console.WriteLine($"Result = {result}");
            Console.WriteLine("----");
        }

        private static int Add(int number1, int number2)
        {
            Console.WriteLine("Add");
            return number1 + number2;
        }

        private static int Subtract(int number1, int number2)
        {
            Console.WriteLine("Subtract");
            return number1 - number2;
        }

        private static int Multiply(int number1, int number2)
        {
            Console.WriteLine("Multiply");
            return number1 * number2;
        }

        private static int Divide(int number1, int number2)
        {
            Console.WriteLine("Divide");
            return number1 / number2;
        }

        #endregion dLG 1
    }
}