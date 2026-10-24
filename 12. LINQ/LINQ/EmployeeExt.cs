using SampleData;
using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    internal static class EmployeeExt
    {
        public static void Filter(this List<Employee> employees, Func<Employee, bool> predicate)
        {
            foreach (Employee employee in employees)
            {
                if (predicate(employee))
                    Console.WriteLine(employee);
            }
        }
    }

}
