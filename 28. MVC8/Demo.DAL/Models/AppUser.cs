namespace Demo.DAL.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
