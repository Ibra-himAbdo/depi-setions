using Delegates;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelpersMethods
{
    internal static class Helpers
    {

        public static IEnumerable<T> Filter<T>(this IEnumerable<T> employees, Predicate<T> predicate)
        {
            List<T> newEmployee = new List<T>();

            foreach (var employee in employees)
            {
                if (predicate(employee))
                    newEmployee.Add(employee);
            }

            return newEmployee;
        }

        public static List<T> ReturnAsList<T>(this IEnumerable<T> items)
        {
            List<T> newItme = new List<T>();

            foreach (var itme in items)
            {
                newItme.Add(itme);
            }

            return newItme;
        }

        public static bool HasItems<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            foreach (var item in items)
            {
                if (predicate(item))
                    return true;
            }

            return false;
        }

    }
}
