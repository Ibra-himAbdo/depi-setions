namespace OOP1;

#region Class1
//int x = 0;
//int y = x;

//Console.WriteLine("x: " + x);
//Console.WriteLine("y: " + y);
//y = 5;
//Console.WriteLine("x: " + x);
//Console.WriteLine("y: " + y);

//Console.WriteLine("----------");

//Employee emp1 = new Employee("Ahmed", 10000, 2020);
//Employee emp2 = emp1; // علامه اليساوي بنقل العنوان مش البيانات

//Employee emp3 = new Employee(emp1);


//Console.WriteLine(emp1.name); 
//Console.WriteLine(emp2.name);
//Console.WriteLine("emp3: " + emp3.name);

//emp2.name = "New name";

//Console.WriteLine(emp1.name);
//Console.WriteLine(emp2.name);
//Console.WriteLine("emp3: " + emp3.name); 
#endregion
#region class1

//class Employee
//{
//    // field
//    public string name;
//    decimal salary;
//    int hireYear;


//    //empty ctor
//    public Employee()
//    {

//    }

//    // paramitrized ctor
//    public Employee(string name, decimal salary, int hireYear)
//    {
//        this.name = name;
//        this.salary = salary;
//        this.hireYear = hireYear;
//    }

//    // copy ctor
//    public Employee(Employee otherEmployee)
//    {
//        name = otherEmployee.name;
//        salary = otherEmployee.salary;
//        hireYear = otherEmployee.hireYear;
//    }

//    public void PrintEmployee()
//    {
//        Console.WriteLine("Name: " + name);
//        Console.WriteLine("Salary: " + salary);
//        Console.WriteLine("Hire Year: " + hireYear);
//        Console.WriteLine("---");
//    }
//} 
#endregion

#region Inhertance
//// upsacling
//Animal animal = new Animal("Elephant");

//Animal cat = new Cat("bosy");

//Animal dog = new Dog("roy");

//Animal lion = new Lion("Simpa");

//animal.PrintName();
//cat.PrintName();
//dog.PrintName();
//lion.PrintName();


//// base class - parent
//class Animal
//{
//    public string Name;

//    public void PrintName()
//    {
//        Console.WriteLine(Name);
//    }

//    public Animal(string name)
//    {
//        Name = name;
//    }
//}

//// drived class - child
//class Cat : Animal
//{
//    public Cat(string name) : base(name)
//    {

//    }
//}

//// drived class - child
//class Dog : Animal
//{
//    public Dog(string name) : base(name)
//    {
//    }
//}

//class An
//{
//    public string Name;
//}

//// Hirearchiacl inhertance
//// drived class - child
//class Lion : Cat
//{
//    public Lion(string name) : base(name)
//    {
//    }
//} 
#endregion

#region struct
//Employee emp1 = new Employee("Ahmed", 1);
//Employee emp2 = emp1; 

//Console.WriteLine(emp1.Name);
//Console.WriteLine(emp2.Name);


//emp2.Name = "New name";

//Console.WriteLine(emp1.Name);
//Console.WriteLine(emp2.Name);


//struct Employee 
//{
//    public int Id;
//    public string Name;

//    public Employee(string name, int id)
//    {
//        Name = name;
//        Id = id;
//    }
//} 
#endregion


internal class Program
{
    private static void Main(string[] args)
    {
        Employee emp1 = new Employee("Ahmed", 1);

        emp1.Name = "554489465465468";

        Console.WriteLine(emp1.Name + " " + emp1.Id);
    }
}

//emp1.Id

public class BaseEntity
{
    internal int Id { get; set; }

    void Print()
    {
        Console.WriteLine(Id);
    }
}


class Employee : BaseEntity
{
    

    public string Name
    {
        get;
        set;
    }



    public Employee(string name, int id)
    {
        Name = name;
        Id = id;
    }

    //public void SetName(string value)
    //{
    //                        // true    // false 
    //    _name = value == "" ? "no value" : value;
    //}
    //public string GetName()
    //{
    //    return _name;
    //}
}

class Student
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Age { get; set; }
}


