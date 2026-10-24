string[] names = ["Ahmed", "Mohamed", "Ibrahim", "Sara"];

string[] products = ["Laptop", "Phone", "Keyboard", "Mouse", "Headset"];
decimal[] productPrices = [25000, 15000, 1200, 700, 1800];
int[] productStock = [10, 20, 30, 50, 15];

#region small store
//// 1 - Laptop - Price: 25000 - Stock: 10
//// 2 - Phone - Price: 15000 - Stock: 20

//for (int i = 0; i < products.Length; i++)
//{
//    if ((i + 1) % 2 == 0)
//    {
//        Console.ForegroundColor = ConsoleColor.Green;
//    }
//    else
//    {
//        Console.ForegroundColor = ConsoleColor.Blue;
//    }

//    Console.WriteLine($"{i + 1} - {products[i]} - Price: {productPrices[i]} - Stock: {productStock[i]}");

//    Console.ResetColor();
//}

//Console.Write("enter products count u want to buy: ");
//int input = int.Parse(Console.ReadLine());

//decimal totalPrice = 0;

//for (int i = 0; i < input; i++)
//{
//    Console.Write($"enter item {i + 1} code: ");
//    int code = int.Parse(Console.ReadLine());

//    if (code <= 0 || code > productPrices.Length)
//    {
//        Console.WriteLine("Invalid code");
//        Console.WriteLine();
//        continue;
//    }

//    Console.Write("how many items you want ? ");
//    int quantity = int.Parse(Console.ReadLine());

//    totalPrice = totalPrice + (productPrices[code - 1] * quantity);

//    Console.WriteLine("Price: " + (productPrices[code - 1] * quantity));
//}

//Console.WriteLine("totalPrice: " + totalPrice); 
#endregion


Console.WriteLine("==============================");
Console.WriteLine("FAST DELIVERY MANAGMENT SYSTEM");
Console.WriteLine("==============================");
Console.WriteLine();

Console.WriteLine("1. Customers Maneagment");
Console.WriteLine("2. Products Maneagment");
Console.WriteLine("3. Orders Maneagment");
Console.WriteLine("4. Exit");

Console.Write("Choose: ");
int userChoose = int.Parse(Console.ReadLine());


switch (userChoose)
{
    case 1:
        Console.WriteLine("Welcome To Customers Maneagment");
        break;

    case 2:
        Console.WriteLine("Welcome To Products Maneagment");
        break;

    case 3:
        Console.WriteLine("Welcome To Orders Maneagment");
        break;

    case 4:
        Console.WriteLine("Goodbye");
        break;

    default:
        Console.WriteLine("invalid number");
        break;

}

