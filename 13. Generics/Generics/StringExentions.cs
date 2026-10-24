namespace Generics
{
    internal static class StringExentions
    {
        // a , e , i , o , u
        public static int CountVowles(this string value)
        {
            int counter = 0;
            foreach (char c in value.ToLower())
            {
                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                {
                    counter++;
                }
            }

            return counter;
        }

        public static string ReverseText(this string text)
        {
            char[] chars = text.ToCharArray();

            Array.Reverse(chars);

            return new string(chars);
        }
    }
}
