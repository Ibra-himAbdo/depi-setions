string empFname = "Ahemd";
string empLname = "Ali";
decimal empSalary = 10000;
int emphireYear = 2020;

string emp2Fname = "Ali";
string emp2Lname = "Ali";
decimal emp2Salary = 8000;
int emp2hireYear = 2024;

string emp3Fname = "Ibrahim";
string emp3Lname = "Mohamed";
decimal emp3Salary = 20000;
int emp3hireYear = 2025;

string emp4Fname = "Ibrahimaaa";
string emp4Lname = "Mohamedaaa";
decimal emp4Salary = 2000220;
int emp4hireYear = 2005;

decimal CalculateBounse(decimal salary)
{
    return salary * 0.10m;
}

int GetYearsOfExp(int hireYear)
{
    return 2026 - hireYear;
}

void PrintEmployee(
    string empFname,
    string empLname,
    decimal empSalary,
    int emphireYear)
{
    Console.WriteLine($"Name: {empFname} {empLname}");
    Console.WriteLine($"Salary: {empSalary + CalculateBounse(empSalary)}");
    Console.WriteLine($"Years of Expierinace: {GetYearsOfExp(emphireYear)}");
    Console.WriteLine("---");
}


PrintEmployee(empFname, empLname, empSalary, emphireYear);
PrintEmployee(emp2Fname, emp2Lname, emp2Salary, emp2hireYear);
PrintEmployee(emp3Fname, emp3Lname, emp3Salary, emp3hireYear);
PrintEmployee(emp4Fname, emp4Lname, emp4Salary, emp4hireYear);