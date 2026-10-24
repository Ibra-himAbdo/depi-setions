using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Generics
{
    public static class IntsExtension
    {
        public static bool IsBetween(this int value, int min, int max)
        {
            return (value >= min && value <= max);
        }

        //public static bool IsEven<T>(this T value) where T : INumber<T>
        //{
        //    return (value % T.Parse(2,out T`) == 0);
        //}

        public static bool IsOdd(this int value)
        {
            return value % 2 != 0;
        }

        public static int Square(this int value)
        {
            return value * value;
        }
    }
}
