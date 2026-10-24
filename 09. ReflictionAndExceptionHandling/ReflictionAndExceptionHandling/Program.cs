namespace ReflictionAndExceptionHandling
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //int[] arr = { 1, 2, 3 };
            //try
            //{
            //    string input = Console.ReadLine();

            //    int x;
            //    if (!int.TryParse(input, out x))
            //    {
            //        Console.WriteLine("Invalid Number Format");
            //        return;
            //    }

            //    int y = 1;

            //    if(y == 0)
            //    {
            //        Console.WriteLine("Cant divied by zero");
            //        return;
            //    }

            //    Console.WriteLine(arr[50]);
            //    Console.WriteLine(x / y);
            //}
            //catch (Exception exception)
            //{
            //    Console.WriteLine(exception);
            //}

            //Student student = null;

            //if (student is not null)
            //    Console.WriteLine(student.Name);

            //try
            //{
            //    RegisterStudent(5);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}

            //try
            //{
            //    string filePath = @"C:\Users\Ibrahim\Desktop\New Text Document (2).txt";

            //    if (File.Exists(filePath))
            //    {
            //        string[] text = File.ReadAllLines(filePath);
            //        foreach (var item in text)
            //        {
            //            Console.WriteLine(item);

            //        }
            //    }
            //    else
            //        Console.WriteLine("File not fund");
            //}
            //catch (FileNotFoundException ex)
            //{
            //    Console.WriteLine(ex);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //}
            try
            {

                throw new MyException();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }

            #region Indexes

            //Console.WriteLine(arr[0]);
            //Console.WriteLine(arr[1]);
            //Console.WriteLine(arr[2]);

            //MyList myList = new MyList();

            //Console.WriteLine(myList[0]);
            //Console.WriteLine(myList["Name 101"]);
            //Console.WriteLine(myList["Name 102"]);

            //myList["Name 102"] = new Student()
            //{
            //    Id = 56656,
            //    Name = "Eslam"
            //};
            //Console.WriteLine(myList[2]);

            #endregion Indexes
        }

        // 18 - 25
        private static void RegisterStudent(int age)
        {
            if (age < 18 || age > 25)
            {
                throw new ArgumentException("Age must be between 18 and 25");
            }
        }
    }

    class MyException : Exception
    {
        public MyException(string mesage = "my Custome message") : base(mesage)
        {

        }
    }

    internal class MyList
    {
        private Student[] _students = new Student[3];

        public MyList()
        {
            for (int i = 0; i < _students.Length; i++)
            {
                Student student = new Student()
                {
                    Id = i + 1,
                    Name = $"Name {i + 100}"
                };
                _students[i] = student;
            }
        }

        public Student this[int index]
        {
            get
            {
                return _students[index];
            }
            set
            {
                _students[index] = value;
            }
        }

        public Student this[string name]
        {
            get
            {
                foreach (var student in _students)
                {
                    if (name == student.Name)
                    {
                        return student;
                    }
                }

                return new Student();
            }
            set
            {
                for (int i = 0; i < _students.Length; i++)
                {
                    if (name == _students[i].Name)
                    {
                        _students[i] = value;
                    }
                }
            }
        }
    }

    internal class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Id: {Id} - Name: {Name}";
        }
    }
}