using Delegates;

namespace HelpersMethods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            List<int> ints = new List<int>();

            int[] arr = new int[100];


            for (int i = 0; i < 100; i++)
            {
                ints.Add(i);
                arr[i] = i;
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


            var newlsit = employees.Filter(e => e.Id > 90);

            foreach (var item in newlsit)
            {
                Console.WriteLine(item.Id);
            }

            foreach (var item in ints.Filter(x => x < 6))
            {
                Console.WriteLine(item);
            }

            List<int> newArray = arr.Filter(x => x > 6).ReturnAsList();

            foreach (var item in newArray)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(newArray.HasItems(x => x > 9));
        }

        
    }
}
