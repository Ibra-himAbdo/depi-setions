Console.Write("Red - Green - yellow : ");
string input = Console.ReadLine();

#region if-else if-else

/*

if (input >= 90 && input > 80 )
{
Console.WriteLine("A");
}
else if (input >= 80 && input > 70)
{
Console.WriteLine("B");
}
else if (input >= 70 && input > 60)
{
Console.WriteLine("C");
}
else
{
Console.WriteLine("u didnt pass");
}
*/

#endregion if-else if-else

//if (input == "Red")
//{
//    Console.WriteLine("Stop");
//}
//else if (input == "Green")
//{
//    Console.WriteLine("Go");
//}
//else if (input == "yellow")
//{
//    Console.WriteLine("Slow");
//}
//else
//{
//    Console.WriteLine("invaled input");
//}

switch (input)
{
    case "Red":
        Console.WriteLine("Stop");
        break;

    case "Green":
        Console.WriteLine("Go");
        break;

    case "yellow":
        Console.WriteLine("Slow");
        break;

    default:
        Console.WriteLine("invaled input");
        break;
}