#region p7

//string s = "race a car";

//////s = s.ToLower();
/////

//string result = "";

//foreach (char c in s)
//{
//    int letterNumber = c;
//    if(letterNumber >= 65 && letterNumber <= 90)
//    {
//        letterNumber += 32;
//        Console.WriteLine((char)letterNumber);
//        result += (char)letterNumber;
//    }
//    else if (letterNumber >= 97 && letterNumber <= 122)
//    {
//        Console.WriteLine(c);
//        result += c;
//    }
//}

//int leftArraw = 0;
//int rightArraw = result.Length - 1;
//bool isMatched = true;

//for (int i = 0; i < result.Length; i++)
//{
//    if (result[leftArraw] != result[rightArraw])
//    {
//        isMatched = false;
//        break;
//    }

//    if(leftArraw > rightArraw)
//    {
//        break;
//    }

//    leftArraw++;
//    rightArraw--;
//}

//Console.WriteLine(s);
//Console.WriteLine(result);

//Console.WriteLine(isMatched);

#endregion

#region Fuctions 1

//Console.WriteLine("=============================");
//Console.WriteLine("  FAST DELIVERY MANAGMENT");
//Console.WriteLine("=============================");

//int choice = 0;
//string[] customers = new string[3] { "Ahmed", "Sara", "😂" };
//string[] products = new string[4] { "P1", "P2", "P3", "P4" };

//void PrintSystemArraies(string message, string[] array)
//{
//    Console.ForegroundColor = ConsoleColor.Green;
//    Console.WriteLine(message);
//    foreach (string itme in array)
//    {
//        Console.WriteLine(itme);
//    }
//    Console.ResetColor();
//}

//bool SeachOnStringArray(string input, string[] array)
//{
//    for (int i = 0; i < array.Length; i++)
//    {
//        if (input == array[i])
//        {
//            return true;
//        }
//    }

//    return false;
//}

//do
//{
//    Console.WriteLine("1. Customers Managment");
//    Console.WriteLine("2. Products Managment");
//    Console.WriteLine("3. Orders Managment");
//    Console.WriteLine("4. Delivery Managment");
//    Console.WriteLine("5. Reports");
//    Console.WriteLine("6. Exit");

//    Console.Write("Choose: ");
//    choice = int.Parse(Console.ReadLine());

//    switch (choice)
//    {
//        case 1:
//            int innerChoice = 0;
//            Console.WriteLine("Welcome to Customers Managment");

//            Console.WriteLine("1. Add new Customers");
//            Console.WriteLine("2. Show System Customers");
//            Console.Write("Enter Choice: ");
//            innerChoice = int.Parse(Console.ReadLine());

//            switch (innerChoice)
//            {
//                case 1:
//                    Console.WriteLine("Add new Customers");
//                    for (int i = 0; i < customers.Length; i++)
//                    {
//                        Console.Write("Enter Customer Name: ");
//                        customers[i] = Console.ReadLine();
//                    }
//                    break;
//                case 2:
//                    PrintSystemArraies("Show Customers Total Count: " + customers.Length, customers);
//                    break;
//                case 3:
//                    Console.WriteLine("Search Customers");
//                    Console.WriteLine("Enter Customer Name: ");
//                    string input = Console.ReadLine();
//                    bool exsit = SeachOnStringArray(input, customers);

//                    Console.ForegroundColor = ConsoleColor.Blue;

//                                              // true case    // false case
//                    Console.WriteLine(exsit ? "We found it" : "Not found");

//                    Console.ResetColor();
//                    break;
//            }
//            break;

//        case 2:
//            Console.WriteLine("Welcome to Products Managment");

//            Console.Write("Enter Choice: ");
//            int innerChoice3 = int.Parse(Console.ReadLine());
//            if(innerChoice3 == 1)
//                PrintSystemArraies("Show Products Total Count: " + products.Length, products);
//            else
//            {
//                string input3 = Console.ReadLine();
//                bool exsit2 = SeachOnStringArray(input3, products);

//                Console.ForegroundColor = ConsoleColor.Blue;

//                                            // true case    // false case
//                Console.WriteLine(exsit2 ? "We found it" : "Not found");

//                Console.ResetColor();
//            }

//            break;

//        case 3:
//            Console.WriteLine("Welcome to Orders Managment");
//            break;

//        case 4:
//            Console.WriteLine("Welcome to Delivery Managment");
//            break;

//        case 5:
//            Console.WriteLine("Welcome to Reports Managment");
//            break;

//        case 6:
//            Console.WriteLine("GoodBye");
//            break;

//        default:
//            Console.WriteLine("invalid choice");
//            break;
//    }
//} while (choice != 6);

#endregion

#region Func 2

//int GetMinOf2(int a, int b)
//{
//    if(a < b)
//    {
//        return a;
//    }
//    else
//    {
//        return b;
//    }
//}

//int GetMinOf3(int a, int b, int c)
//{
//    return GetMinOf2(a, GetMinOf2(b, c));
//}

//int minNumber = GetMinOf3(6, 5,1);

//Console.WriteLine(minNumber);

#endregion

#region Recursion 1

//void Student1()
//{
//    Console.WriteLine("I'm student num 1");
//    Student2();
//    Console.WriteLine("Set 1");
//}
//void Student2()
//{
//    Console.WriteLine("I'm student num 2");
//    Student3();
//    Console.WriteLine("Set 2");
//}

//void Student3()
//{
//    Console.WriteLine("I'm student num 3");
//    Student4();
//    Console.WriteLine("Set 3");
//}

//void Student4()
//{
//    Console.WriteLine("I'm student num 4");
//    Student5();
//    Console.WriteLine("Set 4");
//}
//void Student5()
//{
//    Console.WriteLine("I'm student num 5");
//    Console.WriteLine("Set 5");
//}

//void Student(int n, int max)
//{
//    if (n == max)
//    {
//        Console.WriteLine("I'm student num " + n);
//        Console.WriteLine("Set " + n);
//        return;
//    }

//    Console.WriteLine("I'm student num " + n);
//    Student(n + 1, max);
//    Console.WriteLine("Set " + n);
//}

//Student(1, 50);

//Console.WriteLine("We are Finished!!!"); 
#endregion



// state = n بتاع كل call
// Transition
// base case = return
int Facto(int n)
{               // spical case
    if (n == 1 || n == 0)
    {
        return 1;
    }
    if (n < 0)
    { return -1; }
                    
    return n * Facto(n - 1);
}

Console.WriteLine(Facto(-5562312));