using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
{
    internal static class StringExtensions
    {
        public static int GetStringCount(this string value)
        {
            int count = 0;

            foreach (var c in value)
            {
                count++;
            }

            return count;
        }

        public static int CountVowels(this string text)
        {
            int count = 0;

            foreach (char c in text.ToLower())
            {
                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                {
                    count++;
                }
            }

            return count;
        }

        public static string ReversText(this string text)
        {
            char[] chars = text.ToCharArray();

            Array.Reverse(chars);

            return new string(chars);
            //return text;
        }
    }
}
