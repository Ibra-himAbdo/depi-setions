using SampleData;
namespace LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {


            var emps = Repository.GetEmployees();

            //emps.Filter(employee => employee.Salary > 1_000 && employee.Name.StartsWith("A"));


            //IEnumerable<Employee> q7 = emps.Where(emp => emp.Age > 30);



            //var q15 = emps.Where(emp => emp.HireDate.Year > 2022)
            //    .OrderBy(emp => emp.HireDate.Year)
            //    .ThenBy(e => e.Name).Reverse();



            //string?[] q26 = emps.Select(emp => emp.Name)
            //    .Where(s => s.StartsWith("A")).Order().ToArray();

            //var q28 = emps.Where(e => e.IsActive).Select(emp => new
            //{
            //    EmployeeName = emp.Name,
            //    EmployeeSalary = Convert.ToInt32(emp.Salary)
            //}).Where(x => x.EmployeeSalary > 14000);


            //IEnumerable<List<string>> data1 = emps.Select(e => e.Skills);
            //IEnumerable<string> data2 = emps.SelectMany(e => e.Skills);


            //Console.WriteLine(emp);

            //foreach (var skills in data1)
            //{
            //    foreach (var item in skills)
            //    {
            //        Console.WriteLine(item);
            //    }
            //}

            //Print(data1);
            //Console.WriteLine();
            //Print(data2);


            ////Print(data);



            // sum - avagrage - min - max - Count

            //var data = emps.Count(e => !e.IsActive && e.Age > 30);
            //Console.WriteLine(data);

            //var salarySum = emps.Sum(e => e.Salary);
            //Console.WriteLine(salarySum);

            //var salaryMin = emps.Min(e => e.Salary);
            //Console.WriteLine(salaryMin);

            //var salaryMax = emps.Max(e => e.Salary);
            //Console.WriteLine(salaryMax);

            //var salaryAvrage = emps.Average(e => e.Salary);
            //Console.WriteLine(salaryAvrage);


            //var longestName = emps.Max(e => e.Name.Length);
            //Console.WriteLine(longestName);
            //Console.WriteLine(emps.FirstOrDefault(e => e.Name.Length == longestName));

            //var ex = emps.FirstOrDefault(e => e.Id == 7);

            //Console.WriteLine();

            //var csharpEmployees = emps.Contains(ex);
            //Console.WriteLine(csharpEmployees);
            ////Print(csharpEmployees);


            //Console.WriteLine(emps.SingleOrDefault(e => e.HireDate.Year > 2022));

            //Print(emps.SkipWhile(e => e.IsActive));
            //emps.Clear();
            Console.WriteLine(emps.Count != 0);
        }

        static void Print<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Console.WriteLine(item);
            }
        }

        //static Hmada Map(Employee employee)
        //{
        //    return new Hmada
        //    {
        //        Name = employee.Name,
        //        Salary = Convert.ToInt32(employee.Salary)
        //    };
        //}

        #region FUNCTIONS
        //static bool IsGreaterThan20K(Employee employee)
        //{
        //    return employee.Salary > 20_000;
        //}

        //static bool IsGreaterThan16K(Employee employee)
        //{
        //    return employee.Salary > 16_000;
        //}

        //static bool IsGreaterThan2K(Employee employee)
        //{
        //    return employee.Salary > 2_000;
        //}
        //static bool IsGreaterThan1K(Employee employee)
        //{
        //    return employee.Salary > 1_000;
        //} 
        #endregion


    }

    //class Hmada
    //{
    //    public int Salary { get; set; }
    //    public string? Name { get; set; }

    //    public override string ToString()
    //    {
    //        return $"Name: {Name} - Salary: {Salary}";
    //    }
    //}
}
