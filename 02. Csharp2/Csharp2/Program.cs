#region For1

//Console.Write("Enter Student Count: ");
//int Count = int.Parse(Console.ReadLine());

//for (int i = 1; i <= Count; i++)
//{
//    Console.Write($"Enter Student {i} Name:");
//    string studentName = Console.ReadLine();

//    Console.Write($"Enter Student {i} Age: ");
//    int studentAge = int.Parse(Console.ReadLine());

//    Console.Write($"Enter Student {i} Mark: ");
//    double studentMark = double.Parse(Console.ReadLine());

//    if (studentMark >= 90)
//    {
//        Console.WriteLine("A");
//    }
//    else if (studentMark >= 80 && studentMark < 90)
//    {
//        Console.WriteLine("B");
//    }
//    else
//    {
//        Console.WriteLine("F");
//    }
//}

#endregion

#region For 2

//Console.Write("Enter Houses Count: ");
//int Count = int.Parse(Console.ReadLine());

//for (int i = Count; ; i++)
//{
//    if (i % 2 != 0)
//    {
//        Console.WriteLine($"House number : {i} had visted");
//    }

//    //in case i+= 2
//    //Console.WriteLine($"House number : {i} had visted");
//}

#endregion

#region while1

//int productsCount = 20;
//int vistedHouses = 0;

//while (productsCount > 0)
//{
//    Console.WriteLine("Remaining Products: " + productsCount);

//    Console.Write("Enter number of products deliverd: ");
//    int deliverd = int.Parse(Console.ReadLine());

//    if(deliverd > productsCount)
//    {
//        Console.WriteLine("Cant deliverd more than avalible count : " + productsCount);
//        continue;
//    }

//    if(productsCount <= 0)
//    {
//        break;
//    }

//    productsCount -= deliverd;
//    vistedHouses++;
//}

//Console.WriteLine("Prodcuts VFinished");
//Console.WriteLine("Houses Count : " + vistedHouses);

#endregion

#region While 2

//int n = 1;

//while( n % 16 != 0)
//{
//    Console.WriteLine(n);
//    n++;
//}

//Console.WriteLine("Do you want to Countine?");
//string input  = Console.ReadLine();

//while (input == "yes")
//{
//    Console.WriteLine("Hello While");
//    Console.WriteLine("Do you want to Countine?");
//    input = Console.ReadLine();
//}

#endregion

#region Do While

//string input = "";

//do
//{
//    Console.WriteLine("Hello do While");
//    Console.WriteLine("Do you want to Countine?");
//    input = Console.ReadLine();

//} while (input == "yes");

#endregion

#region Nested

//for (int i = 1; i <= 5; i++)
//{
//    Console.WriteLine("House number " + i);
//    for (int j = 500; j <= 504; j++)
//    {
//        Console.WriteLine("Doc: " + j);
//    }
//    Console.WriteLine("---------------------");
//}

//int j = 500;
//for (int i = 1; i <= 5; i++)
//{
//    Console.WriteLine("House number " + i);
//    Console.WriteLine("Doc: " + j);
//    j++;
//    Console.WriteLine("---------------------");
//}

//int height = 9;
//int width = 5;

//for (int i = 1; i <= height; i++)
//{
//    for (int j = 1; j <= width; j++)
//    {
//        Console.Write(i);
//    }
//    Console.WriteLine();
//}

//for (int i = 0; i < 10000; i++)
//{
//    for (int j = 0; j <= i; j++)
//    {
//        Console.Write("*");
//    }
//    Console.WriteLine();
//}

#endregion

#region Arr1

//int[] students = { 90,80,75,60,50 };

////students[0] = 90;
////students[1] = 80;
////students[2] = 75;
////students[3] = 60;
////students[4] = 50;

//for (int i = 0; i < students.Length; i++)
//{
//    Console.WriteLine(students[i]);
//}
//Console.WriteLine("---------------------");
//Console.WriteLine("---------------------");
//Console.WriteLine("---------------------");

//int student1 = 90;
//int student2 = 80;
//int student3 = 75;
//int student4 = 60;
//int student5 = 50;

//for (int i = 1; i <= 5; i++)
//{
//    Console.WriteLine("student" + i);
//}

//Console.WriteLine(student1);
//Console.WriteLine(student2);
//Console.WriteLine(student3);
//Console.WriteLine(student4);
//Console.WriteLine(student5);

#endregion

#region Arr2

//Console.Write("Enter Student Count: ");

//int count = int.Parse(Console.ReadLine());

//int[] studentMark = new int[count];

//for (int i = 0; i < count; i++)
//{
//    Console.Write($"Enter Student {i + 1} mark: ");
//    studentMark[i] = int.Parse(Console.ReadLine());
//}
//Console.WriteLine("--------------");
//for (int i = 0; i < studentMark.Length; i++)
//{
//    Console.WriteLine(studentMark[i]);
//}

#endregion

string s = "A man, a plan, a canal: Panama";

string result = "";

for (int i = 0; i < s.Length; i++)
{
    int letterNumber = s[i];
    if (letterNumber >= 65 && letterNumber <= 90)
    {
        Console.WriteLine((char)(letterNumber + 32));
        result += (char)(letterNumber + 32);
    }
    else if (letterNumber >= 97 && letterNumber <= 122)
    {
        Console.WriteLine(s[i]);
        result += s[i];
    }
}
Console.WriteLine();
Console.WriteLine(result);
//int number = 'A';
//char c = (char)97;

//Console.WriteLine(number);
//Console.WriteLine(c);


