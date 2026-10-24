namespace SampleData
{
    public class Employee : BaseEntity
    {

        public int Age { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        public DateTime HireDate { get; set; }
        public int DepartmentId { get; set; }
        public List<string> Skills { get; set; } = new();

        public override string ToString()
        {
            return $"[ID: {Id,2}] {Name,-18} | Dept: {DepartmentId,2} | Age: {Age,2} | Salary: {Salary,8:N0} EGP | Active: {IsActive,-5} | Hire: {HireDate:yyyy-MM-dd} | Skills: [{string.Join(", ", Skills)}]";
        }
    }
}
