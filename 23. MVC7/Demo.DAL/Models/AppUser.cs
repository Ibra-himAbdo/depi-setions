namespace Demo.DAL.Models;

//Employee - Department
public class AppUser : IdentityUser
{
    public string? FullName { get; set; }

    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
}
