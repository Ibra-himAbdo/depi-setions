using System;
using System.Collections.Generic;
using System.Text;

namespace Delegates
{
    internal static class ListExt
    {
        public static void ApplyFiltorOnList<T>(this List<T> items, Func<T, bool> predicate)
        {
            foreach (T item in items)
            {
                if (predicate(item))
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
