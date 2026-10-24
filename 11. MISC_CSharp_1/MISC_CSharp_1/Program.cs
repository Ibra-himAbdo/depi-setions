namespace MISC_CSharp_1
{
    // variable scopes
    // 1. Class-level
    // 2. Method-level
    // 3. Block-level
    //----------------

    // Ref & Out keywords

    //------
    //enums
    // FlagEnum
    //---------



    #region Simple Enum
    //public enum Gender
    //{
    //    Male,
    //    Female
    //}

    public enum Color
    {
        Red,
        Blue,
        Yellow,
        Green,
        Maginta
    }

    //public enum UserType
    //{
    //    Admin,
    //    Manger,
    //    NormalUser,
    //} 
    #endregion

    #region FlagEnum
    //[Flags]
    //public enum WeekDays
    //{
    //    None =      0b_0000_0000, //0,
    //    Satureday = 0b_0000_0001, //1 << 0,//1,
    //    Sunday =    0b_0000_0010, //1 << 1,//2,
    //    Monday =    0b_0000_0100, //1 << 2,//4,
    //    Tuseday =   0b_0000_1000, //1 << 3,//8,
    //    Wednesday = 0b_0001_0000, //1 << 4,//16,
    //    Thursday =  0b_0010_0000, //1 << 5,//32,
    //    Friday =    0b_0100_0000, //1 << 6,//64
    //} 
    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            #region Simple enum
            //Console.WriteLine("this is system Colors: ");
            //foreach (string color in Enum.GetNames(typeof(Color)))
            //{
            //    Console.WriteLine($"{color}");
            //}

            //Console.WriteLine("enter color name:");

            //Color color1 = (Color)Enum.Parse(typeof(Color), Console.ReadLine(), true);

            //Console.WriteLine(color1 + " = " + (int)color1); 
            #endregion


            #region FlagEnum
            //WeekDays weekEnd = WeekDays.Friday | WeekDays.Satureday;
            //WeekDays weekDays = WeekDays.Sunday | WeekDays.Monday | WeekDays.Tuseday;


            //WeekDays w1 = WeekDays.Friday | WeekDays.Satureday;
            //WeekDays w2 = WeekDays.Friday | WeekDays.Monday;

            //WeekDays w3 = w1 ^ WeekDays.Monday;
            //WeekDays w4 = w3 ^ WeekDays.Monday;

            //Console.WriteLine(w3);
            //Console.WriteLine(w4);

            //if (w1.HasFlag(WeekDays.Monday))
            //{
            //    Console.WriteLine("sadasadasd");
            //}

            ////Console.WriteLine(w1 & w2);
            ////Console.WriteLine(weekDays); 
            #endregion

            // inherted from object class
            // 1. public virual string ToString();
            // 2. public virual int GetHashCode(); // unique code
            // 3. public virual bool Equals();
            // 4. public Type GetType()


            int x = 5;

            object y = x; // boxing

            int z = (int)y; // unboxing

            Console.WriteLine(y);

            Console.WriteLine("----------");


            object employee1 = new Employee()
            {
                Id = 1,
                Name = "Test",
            };

            Console.WriteLine(employee1);

            Employee employee3 = (Employee)employee1;

            Console.WriteLine(employee3);


            Employee employee2 = new Employee()
            {
                Id = 1,
                Name = "Test",
            };

            Console.WriteLine(employee1.Equals(employee2));

            Console.WriteLine("-------------");


            MyList myList = new MyList();

            Book book = new Book()
            {
                Title = "Test1",
                Auther = "name1"
            };
            Book book2 = new Book()
            {
                Title = "Test2",
                Auther = "name2"
            };
            Book book3 = new Book()
            {
                Title = "Test3",
                Auther = "name3"
            };
            Book book4 = new Book()
            {
                Title = "Test4",
                Auther = "name4"
            };

            myList.Add(book);
            myList.Add(book2);
            myList.Add(book3);
            myList.Add(book4);

            Book book5 = new Book()
            {
                Title = "Test5",
                Auther = "name5"
            };


            Console.WriteLine(myList[0]);
            myList[1] = book5;

            Console.WriteLine(myList[1]);

            Console.WriteLine("---");
            myList["Test5"] = book2;
            Console.WriteLine(myList["Test2"]);
            Console.WriteLine(myList["Test6+56"]);
            Console.WriteLine("---");
            Console.WriteLine(myList.Count);
            Console.WriteLine();

            myList.PrintAll();


        }
    }

    class BaseEntity
    {
        public int Id { get; set; }
    }
    class Employee : BaseEntity
    {

        public string Name { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj is Employee other)
            {
                //Employee other = (Employee)obj;

                if (other.Id != Id) return false;
                if (other.Name != Name) return false;

                return true;
            }

            return false;
        }


        public override int GetHashCode()
        {
            return Name.GetHashCode() + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Id: {Id} - Name: {Name}";
        }
    }

    public class Book
    {
        public string Title { get; set; }
        public string Auther { get; set; }

        public override string ToString()
        {
            return $"Title: {Title} - Auther: {Auther}";
        }
    }

    public class MyList
    {
        Book[] _books = new Book[1];
        int _count = 0;


        public int Count { get { return _count; } }

        public Book this[int index]
        {
            get { return _books[index]; }
            set { _books[index] = value; }
        }

        public Book this[string title]
        {
            get
            {
                foreach (Book book in _books)
                {
                    if (book.Title == title)
                    {
                        return book;
                    }
                }


                return new Book();
            }
            set
            {
                for (int i = 0; i < _books.Length; i++)
                {
                    if (_books[i].Title == title)
                    {
                        _books[i] = value;
                    }
                }

            }
        }


        public void Add(Book newBook)
        {
            if (_count == _books.Length)
            {
                Book[] newBooks = new Book[2 * _books.Length];

                for (int i = 0; i < _books.Length; i++)
                {
                    newBooks[i] = _books[i];
                }

                _books = newBooks;
            }

            _books[_count] = newBook;
            _count++;
        }

        public void PrintAll()
        {
            foreach (Book book in _books)
            {
                Console.WriteLine(book);
            }
        }
    }



}