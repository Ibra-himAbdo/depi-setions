#region MyRegion
//string[] customers = ["Ahmed", "Mohamed", "Ibrahim", "Sara"];

//string[] products = ["Laptop", "Phone", "Keyboard", "Mouse", "Headset"];
//decimal[] productPrices = [25000, 15000, 1200, 700, 1800];
//int[] productStock = [10, 20, 30, 50, 15];

//void PrintArray(string[] array)
//{
//    Console.ForegroundColor = ConsoleColor.Blue;
//    foreach (string item in array)
//    {
//        Console.WriteLine(item);
//    }
//    Console.ResetColor();
//}



//int userChoose;
//do
//{
//    Console.WriteLine("==============================");
//    Console.WriteLine("FAST DELIVERY MANAGMENT SYSTEM");
//    Console.WriteLine("==============================");
//    Console.WriteLine();

//    Console.WriteLine("1. Customers Maneagment");
//    Console.WriteLine("2. Products Maneagment");
//    Console.WriteLine("3. Orders Maneagment");
//    Console.WriteLine("4. Exit");

//    Console.Write("Choose: ");
//    userChoose = int.Parse(Console.ReadLine());

//    switch (userChoose)
//    {
//        case 1:
//            Console.WriteLine("Welcome To Customers Maneagment");

//            Console.WriteLine("1. Add new Customer");
//            Console.WriteLine("2. Show All Customers");
//            Console.WriteLine("3. Search for Customer");

//            int userChoose2 = int.Parse(Console.ReadLine());

//            switch (userChoose2)
//            {
//                case 1:
//                    Console.Write("Enter new Customer: ");
//                    Console.ReadLine();
//                    break;

//                case 2:
//                    PrintArray(customers);
//                    break;

//                case 3:

//                    Console.Write("Enter Customer name: ");
//                    string input = Console.ReadLine();

//                    for (int i = 0; i < customers.Length; i++)
//                    {
//                        if (input == customers[i])
//                        {
//                            Console.ForegroundColor = ConsoleColor.Green;
//                            Console.WriteLine("we found custmer: " + customers[i]);
//                            Console.ResetColor();
//                        }
//                        else
//                        {
//                            Console.WriteLine("Not Found");
//                            Console.WriteLine();
//                            break;
//                        }
//                    }

//                    break;

//                default:
//                    break;
//            }

//            break;

//        case 2:
//            Console.WriteLine("Welcome To Products Maneagment");

//            Console.WriteLine("-------------");
//            PrintArray(products);
//            Console.WriteLine("-------------");
//            Console.WriteLine();
//            break;

//        case 3:
//            Console.WriteLine("Welcome To Orders Maneagment");
//            break;

//        case 4:
//            Console.WriteLine("Goodbye");
//            break;

//        default:
//            Console.WriteLine("invalid number");
//            break;
//    }
//} while (userChoose != 4); 
#endregion



#region func1
//int GetMinOf2(int a, int b)
//{
//    Console.WriteLine("2nd");
//    if (a < b)
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
//    return GetMinOf2(c, GetMinOf2(a, b));
//}

//Console.WriteLine("1st");

//Console.WriteLine(GetMinOf3(-1, 2, 5)); 
#endregion


//void Student01()
//{
//    Console.WriteLine("I'm Student01");
//    Student02();
//    Console.WriteLine("set 1");
//}
//void Student02()
//{
//    Console.WriteLine("I'm Student02");
//    Student03();
//    Console.WriteLine("set 2");
//}

//void Student03()
//{
//    Console.WriteLine("I'm Student03");
//    Student04();
//    Console.WriteLine("set 3");
//}

//void Student04()
//{
//    Console.WriteLine("I'm Student04");
//    Student05();
//    Console.WriteLine("set 4");
//}

//void Student05()
//{
//    Console.WriteLine("I'm Student05");
//    Console.WriteLine("set 5");
//}

//Student01();

void Student(int n)
{
    if(n == 5)
    {
        Console.WriteLine("I'm Student05--");
        Console.WriteLine("set 5--");
        return;
    }

    Console.WriteLine("I'm Student0" + n);
    Student(n + 1);
    Console.WriteLine("set " + n);
}

Student(1);