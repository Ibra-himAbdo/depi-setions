using System.Numerics;

namespace Generics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Ex Methods
            //    int presentage = 5;

            //    if (presentage.IsBetween(0, 100))
            //    {
            //        Console.WriteLine("Valid");
            //    }


            //    int age = 20;

            //    // 18 - 25
            //    //if (IsNumberInRange(age, 18, 25))
            //    //if (IntsExtension.IsBetween(age,18, 25))
            //    if (age.IsBetween(18, 25))
            //    {
            //        Console.WriteLine("Valid");
            //    }

            //int x = 4;

            //if(x.IsOdd())
            //{
            //    Console.WriteLine("odd");
            //}
            //else
            //    Console.WriteLine("even");

            //Console.WriteLine(x.Square());

            //string s = "aHmEd";

            //Console.WriteLine(s.GetStringCount());

            //Console.WriteLine(s.CountVowels());
            //Console.WriteLine(GetVowels(s));
            //Console.WriteLine(s.ReversText().ToUpper().TrimEnd('A').TrimStart('D'));

            //int[] arr = { 1, 2, 3 }; 

            //Console.WriteLine(string.Join("", arr));

            //Console.WriteLine(s.ReversText()); 
            #endregion

            //Product<int, string> product1 = new Product<int, string>()
            //{
            //    Id = 1,
            //    Name = "product1"
            //};
            //Console.WriteLine(product1);

            //Product<string, string> product2 = new Product<string, string>()
            //{
            //    Id = "P14",
            //    Name = "product2"
            //};

            //Console.WriteLine(product2);

            //Product<string, int> product3 = new Product<string, int>()
            //{

            //    Id = "P14",
            //    Name = 56586
            //};
            //Console.WriteLine(product3);

            //Product<int, int> product4 = new Product<int, int>()
            //{
            //    Id = 14,
            //    Name = 56555
            //};
            //Console.WriteLine(product4);

            //Product<char, int> product5 = new Product<char, int>()
            //{
            //    Id = 'A',
            //    Name = 56555
            //};
            //Console.WriteLine(product5);

            //Box<int> box = new(5);

            //Print(product5);
            //Print(box);
            //Print(1);
            //Print(2.5);


            //int[] arr = new int[] { 1, 2, 3 };
            //double[] arrd = new double[] { 1.54, 2.65, 3.8526 };
            //Console.WriteLine(Sum(arrd));



        }

        static T Sum<T>(T[] objects) where T : INumber<T>
        {
            T sum = T.Zero;
            foreach (var item in objects)
            {
                sum += item;
            }

            return sum;
        }


        static void Print<T>(T value) where T : class, new()
        {
            Console.WriteLine(value);
        }


        #region MyRegion
        // a,e,i,o,u
        //static string GetVowels(string text)
        //{
        //    string vowels = "";

        //    foreach (char c in text.ToLower())
        //    {
        //        if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
        //        {
        //            vowels += $"{c} - ";
        //        }
        //    }

        //    return vowels;
        //} 
        #endregion


    }

    class BaseEntity
    {

    }

    class Stundet : BaseEntity
    {
        public int Id { get; set; }
    }

    class Teacher : BaseEntity
    {
        public int Id { get; set; }
    }

    interface IRepository<T> where T : BaseEntity
    {
        void Add(T item);
        T Get(int index);
    }

    class TeacherRepository : IRepository<Teacher>
    {
        private Teacher[] _stundets = new Teacher[1];
        public void Add(Teacher item)
        {
            _stundets[0] = item;
        }

        public Teacher Get(int index)
        {
            return _stundets[index];
        }
    }



    class Box<T>
    {
        public T Value { get; set; }

        public Box(int x)
        {

        }

        public override string ToString()
        {
            return $"value: {Value}";
        }
    }

    class Product<TId, TName>
    {
        public TId Id { get; set; }
        public TName Name { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - Name: {Name}";
        }
    }
}
