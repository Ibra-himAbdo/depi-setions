namespace SampleData
{
    public static class Repository
    {
        public static List<Department> Departments { get; } = new()
        {
            new Department { Id = 10, Name = "Information Technology", ManagerId = 1 },
            new Department { Id = 20, Name = "Human Resources",        ManagerId = 8 },
            new Department { Id = 30, Name = "Finance & Accounting",   ManagerId = 12 },
            new Department { Id = 40, Name = "Sales & Marketing",      ManagerId = 16 },
            new Department { Id = 50, Name = "Quality Assurance",      ManagerId = 22 },
            new Department { Id = 60, Name = "Research & Development", ManagerId = 0 } // Empty department (No employees assigned) -> Great for GroupJoin & Left Join demos!
        };

        public static List<Employee> Employees { get; } = new()
        {
            // === IT Department (Dept 10) ===
            new Employee
            {
                Id = 1,
                Name = "Ahmed Ali",
                Age = 38,
                Salary = 24000,
                IsActive = true,
                HireDate = new DateTime(2018, 3, 15),
                DepartmentId = 10,
                Skills = new() { "C#", "SQL", "Architecture", "Azure", "Docker" }
            },
            new Employee
            {
                Id = 2,
                Name = "Sara Hassan",
                Age = 29,
                Salary = 16500,
                IsActive = true,
                HireDate = new DateTime(2020, 7, 1),
                DepartmentId = 10,
                Skills = new() { "C#", "ASP.NET Core", "SQL", "Angular" }
            },
            new Employee
            {
                Id = 3,
                Name = "Mohamed Ibrahim",
                Age = 25,
                Salary = 9500,
                IsActive = true,
                HireDate = new DateTime(2022, 11, 10),
                DepartmentId = 10,
                Skills = new() { "C#", "SQL", "JavaScript" }
            },
            new Employee
            {
                Id = 4,
                Name = "Omar Khaled",
                Age = 32,
                Salary = 18000,
                IsActive = true,
                HireDate = new DateTime(2019, 5, 20),
                DepartmentId = 10,
                Skills = new() { "Python", "Machine Learning", "SQL", "Docker" }
            },
            new Employee
            {
                Id = 5,
                Name = "Mariam Adel",
                Age = 24,
                Salary = 8000,
                IsActive = true,
                HireDate = new DateTime(2023, 1, 15),
                DepartmentId = 10,
                Skills = new() { "React", "JavaScript", "TypeScript", "HTML/CSS" }
            },
            new Employee
            {
                Id = 6,
                Name = "Mostafa Mahmoud",
                Age = 41,
                Salary = 22000,
                IsActive = false,
                HireDate = new DateTime(2017, 9, 12),
                DepartmentId = 10,
                Skills = new() { "C#", "Azure", "Kubernetes", "DevOps" }
            },
            new Employee
            {
                Id = 7,
                Name = "Nourhan Tarek",
                Age = 23,
                Salary = 6500,
                IsActive = true,
                HireDate = new DateTime(2023, 8, 1),
                DepartmentId = 10,
                Skills = new() { "C#", "SQL" }
            },

            // === HR Department (Dept 20) ===
            new Employee
            {
                Id = 8,
                Name = "Mona Zaki",
                Age = 36,
                Salary = 19000,
                IsActive = true,
                HireDate = new DateTime(2018, 10, 5),
                DepartmentId = 20,
                Skills = new() { "HR Management", "Recruitment", "Communication", "Labor Law" }
            },
            new Employee
            {
                Id = 9,
                Name = "Amr Nabil",
                Age = 30,
                Salary = 12000,
                IsActive = true,
                HireDate = new DateTime(2021, 3, 10),
                DepartmentId = 20,
                Skills = new() { "Recruitment", "Interviewing", "Onboarding" }
            },
            new Employee
            {
                Id = 10,
                Name = "Dina Samir",
                Age = 27,
                Salary = 10500,
                IsActive = true,
                HireDate = new DateTime(2022, 4, 18),
                DepartmentId = 20,
                Skills = new() { "Payroll", "HR Analytics", "Excel" }
            },
            new Employee
            {
                Id = 11,
                Name = "Reem Youssef",
                Age = 24,
                Salary = 7000,
                IsActive = false,
                HireDate = new DateTime(2023, 6, 1),
                DepartmentId = 20,
                Skills = new() { "Communication", "Administration", "English" }
            },

            // === Finance & Accounting (Dept 30) ===
            new Employee
            {
                Id = 12,
                Name = "Tarek Mansour",
                Age = 45,
                Salary = 25000,
                IsActive = true,
                HireDate = new DateTime(2016, 2, 1),
                DepartmentId = 30,
                Skills = new() { "Financial Analysis", "Budgeting", "Auditing", "Excel", "ERP" }
            },
            new Employee
            {
                Id = 13,
                Name = "Salma Ezzat",
                Age = 31,
                Salary = 14000,
                IsActive = true,
                HireDate = new DateTime(2020, 9, 15),
                DepartmentId = 30,
                Skills = new() { "Taxation", "Accounting", "Excel", "ERP" }
            },
            new Employee
            {
                Id = 14,
                Name = "Khaled Fawzy",
                Age = 28,
                Salary = 11000,
                IsActive = true,
                HireDate = new DateTime(2021, 12, 1),
                DepartmentId = 30,
                Skills = new() { "Financial Modeling", "Excel", "PowerBI" }
            },
            new Employee
            {
                Id = 15,
                Name = "Aya Mostafa",
                Age = 26,
                Salary = 8500,
                IsActive = true,
                HireDate = new DateTime(2022, 8, 20),
                DepartmentId = 30,
                Skills = new() { "Bookkeeping", "Invoicing", "Excel" }
            },

            // === Sales & Marketing (Dept 40) ===
            new Employee
            {
                Id = 16,
                Name = "Karim El-Sayed",
                Age = 39,
                Salary = 21000,
                IsActive = true,
                HireDate = new DateTime(2017, 11, 15),
                DepartmentId = 40,
                Skills = new() { "B2B Sales", "Negotiation", "CRM", "Strategy" }
            },
            new Employee
            {
                Id = 17,
                Name = "Hany Shaker",
                Age = 34,
                Salary = 15000,
                IsActive = true,
                HireDate = new DateTime(2019, 8, 10),
                DepartmentId = 40,
                Skills = new() { "Digital Marketing", "SEO", "Content Strategy", "Google Ads" }
            },
            new Employee
            {
                Id = 18,
                Name = "Farida Kamal",
                Age = 29,
                Salary = 13500,
                IsActive = true,
                HireDate = new DateTime(2021, 6, 5),
                DepartmentId = 40,
                Skills = new() { "Social Media", "Graphic Design", "Branding", "Copywriting" }
            },
            new Employee
            {
                Id = 19,
                Name = "Ziad Hamdy",
                Age = 26,
                Salary = 9000,
                IsActive = true,
                HireDate = new DateTime(2022, 10, 12),
                DepartmentId = 40,
                Skills = new() { "Direct Sales", "Cold Calling", "CRM" }
            },
            new Employee
            {
                Id = 20,
                Name = "Layla Sherif",
                Age = 24,
                Salary = 8000,
                IsActive = false,
                HireDate = new DateTime(2023, 4, 1),
                DepartmentId = 40,
                Skills = new() { "Market Research", "Excel", "Presentation" }
            },
            new Employee
            {
                Id = 21,
                Name = "Mahmoud Reda",
                Age = 30,
                Salary = 12500,
                IsActive = true,
                HireDate = new DateTime(2020, 11, 25),
                DepartmentId = 40,
                Skills = new() { "B2B Sales", "Account Management", "Negotiation" }
            },

            // === Quality Assurance (Dept 50) ===
            new Employee
            {
                Id = 22,
                Name = "Yasmine Fathy",
                Age = 35,
                Salary = 18500,
                IsActive = true,
                HireDate = new DateTime(2019, 1, 20),
                DepartmentId = 50,
                Skills = new() { "Manual Testing", "Test Automation", "Selenium", "C#", "Jira" }
            },
            new Employee
            {
                Id = 23,
                Name = "Ibrahim Hamed",
                Age = 28,
                Salary = 12000,
                IsActive = true,
                HireDate = new DateTime(2021, 7, 15),
                DepartmentId = 50,
                Skills = new() { "API Testing", "Postman", "C#", "SQL" }
            },
            new Employee
            {
                Id = 24,
                Name = "Radwa Soliman",
                Age = 25,
                Salary = 8500,
                IsActive = true,
                HireDate = new DateTime(2023, 2, 10),
                DepartmentId = 50,
                Skills = new() { "Manual Testing", "Bug Tracking", "Jira", "SQL" }
            },
            new Employee
            {
                Id = 25,
                Name = "Hadeer Magdy",
                Age = 27,
                Salary = 11000,
                IsActive = false,
                HireDate = new DateTime(2022, 5, 1),
                DepartmentId = 50,
                Skills = new() { "Performance Testing", "JMeter", "Postman" }
            },

            // === Additional Cross-Department / Edge-Case Records ===
            new Employee
            {
                Id = 26,
                Name = "Sherif Salem",
                Age = 33,
                Salary = 17000,
                IsActive = true,
                HireDate = new DateTime(2020, 3, 1),
                DepartmentId = 10,
                Skills = new() { "C#", "ASP.NET Core", "SQL", "Microservices" }
            },
            new Employee
            {
                Id = 27,
                Name = "Nada Essam",
                Age = 22,
                Salary = 5000,
                IsActive = true,
                HireDate = new DateTime(2024, 2, 1),
                DepartmentId = 10,
                Skills = new() { "C#", "OOP", "SQL" }
            },
            new Employee
            {
                Id = 28,
                Name = "Wael Badr",
                Age = 48,
                Salary = 23500,
                IsActive = true,
                HireDate = new DateTime(2015, 6, 15),
                DepartmentId = 30,
                Skills = new() { "Risk Management", "Corporate Finance", "Auditing", "ERP" }
            },
            new Employee
            {
                Id = 29,
                Name = "Heba Osman",
                Age = 25,
                Salary = 8000,
                IsActive = true,
                HireDate = new DateTime(2023, 9, 15),
                DepartmentId = 20,
                Skills = new() { "Employee Relations", "Communication", "Event Planning" }
            },
            new Employee
            {
                Id = 30,
                Name = "Bassam Ashraf",
                Age = 23,
                Salary = 4500,
                IsActive = true,
                HireDate = new DateTime(2024, 3, 1),
                DepartmentId = 0, // Unassigned / Intern (No Department) -> Great for Inner Join vs Left Outer Join demos!
                Skills = new() { "C#", "Python", "Git" }
            }
        };

        // Convenient Helper Methods
        public static List<Department> GetDepartments() => Departments;
        public static List<Employee> GetEmployees() => Employees;
    }
}
