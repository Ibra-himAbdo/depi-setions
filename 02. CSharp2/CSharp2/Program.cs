#region ex1
//for (int i = 1; i <= 5; i++)
//{
//    Console.WriteLine($"{i} - ABC Training Center");
//}


//for (int i = 1; i <= 5; i++)
//{
//    #region Q1
//    Console.Write("enter student Id: ");
//    int studentId = int.Parse(Console.ReadLine());
//    Console.Write("enter student name: ");
//    string studentName = Console.ReadLine();
//    Console.Write("enter student age: ");
//    int age = int.Parse(Console.ReadLine());
//    #endregion

//    #region ConstDate
//    string trackName = "Software Development";
//    bool isActive = true;
//    char grade = 'F';
//    #endregion

//    Console.Write("enter Stundet Mark : ");
//    double gradeNumber = double.Parse(Console.ReadLine());

//    if (age > 25 && age < 18)
//    {
//        Console.WriteLine("Invalid age value student age must be in renge 18 to 25.");
//        return;
//    }

//    if (gradeNumber <= 100 && gradeNumber >= 90)
//    {
//        grade = 'A';
//    }
//    else if (gradeNumber <= 89 && gradeNumber >= 80)
//    {
//        grade = 'B';
//    }
//    else if (gradeNumber <= 79 && gradeNumber >= 70)
//    {
//        grade = 'C';
//    }
//    else if (gradeNumber <= 69 && gradeNumber >= 60)
//    {
//        grade = 'D';
//    }
//    else
//        grade = 'F';


//    Console.WriteLine("Student ID: " + studentId);
//    Console.WriteLine("Student Name: " + studentName);
//    Console.WriteLine("Age: " + age);
//    Console.WriteLine("track Name: " + trackName);
//    Console.WriteLine("grade: " + grade);
//} 
#endregion
#region for1

//int m1 = 0;
//int m2 = 0;


//for (int i = 5; i >= 1; i--)
//{
//    if (i % 2 != 0)
//    {
//        Console.WriteLine($"m1 => House num {i} visted");
//        m1++;
//    }
//    else
//    {
//        Console.WriteLine($"m2 => House num {i} visted");
//        m2++;
//    }
//}

//Console.WriteLine(m1);
//Console.WriteLine(m2); 
#endregion

#region While 01
//int productsCount = 20;
//int HousesCount = 0;

//while (productsCount > 0)
//{
//    Console.WriteLine("Product remainig: " + productsCount);

//    Console.Write("Enter Number of Products deliverd: ");
//    int deliverd = int.Parse(Console.ReadLine());

//    if (deliverd <= 0)
//    {
//        Console.WriteLine("Invalid Number.");
//    }
//    else if (deliverd > productsCount)
//    {
//        Console.WriteLine($"cant take bigger than avalible quntity: {productsCount}");
//        Console.WriteLine("-----------");

//    }
//    else
//    {
//        productsCount -= deliverd;
//        HousesCount++;
//    }
//}

//Console.WriteLine("We had finished");
//Console.WriteLine("visted houses count : " + HousesCount); 
#endregion

#region while 2
//int num = 1;
//while (num % 16 != 0)
//{
//    Console.WriteLine(num);
//    num++;
//}

//Console.WriteLine("Hello While");
//Console.WriteLine("Do you Want to Countine");
//string input = Console.ReadLine();

//while(input == "yes")
//{
//    Console.WriteLine("Hello While");
//    Console.WriteLine("Do you Want to Countine");
//    input = Console.ReadLine();
//} 
#endregion

#region Do While
//string input = "";
//do
//{
//    Console.WriteLine("Hello While");
//    Console.WriteLine("Do you Want to Countine");
//    input = Console.ReadLine();
//}
//while (input == "yes"); 
#endregion

#region Nested 1
//for (int i = 1; i <= 5; i++)
//{
//    Console.WriteLine("House " + i);
//    for (int j = 500; j <= 505; j++)
//    {
//        Console.WriteLine("Doc num " + j);
//    }
//    Console.WriteLine("+-++++-+-+-++-+-+-+-+");
//}
#endregion

#region nested2
//int height = 9;

//for (int i = 1; i <= height; i++)
//{
//    for (int j = 1; j <= i; j++)
//    {
//        Console.Write("*");
//    }
//    Console.WriteLine();
//} 
#endregion

//----------------------------------------

#region Array1
////int[] scoures = [90, 85, 70, 60, 40];

//int[] scoures = { 90,85,70,60,40 };


//for(int i = 0; i < scoures.Length; i++)
//{
//    Console.WriteLine(scoures[i]);
//}

//Console.WriteLine("----------");
//int scoure1 = 90;
//int scoure2 = 85;
//int scoure3 = 70;
//int scoure4 = 60;
//int scoure5 = 45;

//Console.WriteLine(scoure1);
//Console.WriteLine(scoure2);
//Console.WriteLine(scoure3);
//Console.WriteLine(scoure4);
//Console.WriteLine(scoure5); 
#endregion

#region array 2
//Console.Write("enter student Number: ");
//int count = int.Parse(Console.ReadLine());
//Console.WriteLine();

//int[] degrees = new int[count];

//Console.WriteLine(degrees.Length == count);

//for (int i = 0; i < degrees.Length; i++)
//{
//    Console.Write($"Enter degree {i + 1} :");
//    degrees[i] = int.Parse(Console.ReadLine());
//}

//Console.WriteLine("---------------");
//for (int i = 0; i < count; i++)
//{
//    Console.WriteLine(degrees[i]);
//} 
#endregion 


