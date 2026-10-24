using System.Collections;
using System.Numerics;

namespace Generics
{
    // Topics
    // Exention Methods
    // Generics
    // Collections

    internal class Program
    {
        static void Main(string[] args)
        {
            //int precintage = 0;

            //int age = 0;
            //// 18 - 25

            //// 0 => 100%

            ////precintage.IsBetween(0, 100);

            ////if (Helpers.IsNumberInRange(precintage, 0, 100))
            //if (precintage.IsBetween(0, 100))
            //{
            //    Console.WriteLine("Corrent precinteage");
            //}
            //else
            //{
            //    Console.WriteLine("Not Corrent precinteage");
            //}

            //Console.WriteLine(age.IsBetween(18, 25));



            //int x = 4;


            //if (ProgramExentions.IsEven(x) && x.IsEven())
            //    Console.WriteLine("even");
            //else
            //    Console.WriteLine("odd");


            //Console.WriteLine("Aello".CountVowles());

            //Console.WriteLine("Hello World!".ReverseText());
            //Console.WriteLine("Hello World!".ReverseText().ToUpper());

            //decimal c = 0;
            //Console.WriteLine(c.IsBetween(100,5));



            Product<int, string> p1 = new Product<int, string>()
            {
                Id = 1,
                Name = "Test1",
            };

            Console.WriteLine(p1);

            Product<string, string> p2 = new Product<string, string>()
            {
                Id = "C2",
                Name = "Test2"
            };

            Console.WriteLine(p2);

            Product<string, int> p3 = new()
            {
                Id = "A3",
                Name = 65465325
            };
            Console.WriteLine(p3);

            Product<char, int> p4 = new()
            {
                Id = 'A',
                Name = 65465325
            };


            //Print(p4);
            //Print<int>(5332);
            Print(5332);
            Print(5332.255);
            Print(5332255l);
            //Print("5332255l");

            Box<Product<char, int>> box = new()
            {
                Value = p4
            };

            //Box<int> box2 = new()
            //{
            //    Value = 10
            //};

            MyGenericCollection<int> collection1 = new MyGenericCollection<int>();

            collection1.Add(1);
            collection1.Add(2);
            collection1.Add(3);

            foreach (int item in collection1)
            {
                Console.WriteLine(item);
            }

            //MyGenericCollection collection = new MyGenericCollection(typeof(int));

            //collection.Add("dasdasd");

        }

        static void Print<T>(T value) where T : INumber<T>
        {
            Console.WriteLine(value);
        }


    }

    class MyGenericCollection<T> : IEnumerable<T>
    {
        private T[] _items = new T[1];

        private int _count = 0;

        public void Add(T item)
        {
            if (_count == _items.Length)
            {
                T[] newItems = new T[_items.Length * 2];

                for (int i = 0; i < _items.Length; i++)
                {
                    newItems[i] = _items[i];
                }
                _items = newItems;
            }


            _items[_count] = item;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                yield return _items[i];
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                yield return _items[i];
            }
        }
    }


    //class MyCollection
    //{
    //    private object[] _items;

    //    private int _count = 0;

    //    private Type _type;

    //    public MyCollection(Type type)
    //    {
    //        _type = type;
    //    }

    //    public void Add(object item)
    //    {
    //        if (item.GetType() != _type)
    //        {
    //            throw new ArgumentException($"we accpet only {_type.Name}");
    //        }

    //        //if (_items is null)
    //        //{
    //        //    _items = new int[_count];
    //        //}
    //        _items ??= new object[_count];

    //        //if()


    //        _items[_count] = item;
    //    }
    //}




    //interface IRepository<T> where T : BaseEntity, new()
    //{
    //    void Add(T item);
    //    T Get(int indx);
    //}



    //class StudnetRepository : IRepository<Studenet>
    //{
    //    public void Add(Studenet item)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Studenet Get(int indx)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //class BaseEntity
    //{
    //    public int Id { get; set; }
    //}

    //class Studenet : BaseEntity
    //{

    //}

    class Studenet2
    {

    }


    public class Box<T> where T : class, new()
    {
        public T Value { get; set; }
    }

    class Pair<T, U>
    {
        public T Frist { get; set; }
        public U Second { get; set; }
    }

    class Product<T, U>
    {
        public T Id { get; set; }
        public U Name { get; set; }


        public Product()
        {

        }

        public Product(T id)
        {

        }

        public override string ToString()
        {
            return $"ID: {Id} - Name: {Name}";
        }
    }

    //class ProductWithStringID
    //{
    //    public string Id { get; set; }
    //    public string Name { get; set; }

    //    public override string ToString()
    //    {
    //        return $"ID: {Id} - Name: {Name}";
    //    }
    //}

    //class ProductWithIntIdAndSriallName
    //{
    //    public string Id { get; set; }
    //    public int Name { get; set; }

    //    public override string ToString()
    //    {
    //        return $"ID: {Id} - Name: {Name}";
    //    }
    //}
}
