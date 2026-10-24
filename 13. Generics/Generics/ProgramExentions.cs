using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    internal static class ProgramExentions
    {
        //double - decmal - float - short - Long -
        //public static bool IsBetween<T>(this T value, T min, T max)
        //{
        //    return value >= min && value <= max;
        //}
        //public static bool IsBetween(this double value, double min, double max)
        //{
        //    return value >= min && value <= max;
        //}
        //public static bool IsBetween(this decimal value, decimal min, decimal max)
        //{
        //    return value >= min && value <= max;
        //}
        //public static bool IsBetween(this long value, long min, long max)
        //{
        //    return value >= min && value <= max;
        //}

        public static bool IsEven(this int value)
        {
            return (value % 2 == 0);
        }
    }
}
