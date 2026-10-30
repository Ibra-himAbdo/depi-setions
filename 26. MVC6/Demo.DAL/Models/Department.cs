namespace Demo.DAL.Models;

public class Department
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public DateTime DateOfCreation { get; set; }

    [NotMapped] public DateTime? StartDate { get; set; }

    [NotMapped] public DateTime? EndDate { get; set; }
}

// old 
//public class Department
//{
//    public int Id { get; set; }

//    [Required(ErrorMessage = "Name is required")]
//    public string? Name { get; set; }

//    [Required(ErrorMessage = "Code is required!!!!!!!")]
//    public string? Code { get; set; }

//    [NoFutureDate(ErrorMessage = "No Future Date")]
//    [Display(Name = "Date Of Creation")]
//    public DateTime DateOfCreation { get; set; }

//    [NotMapped]
//    [Display(Name = "Start Date")]
//    public DateTime? StartDate { get; set; }

//    [NotMapped]
//    [AfterDate(nameof(StartDate))]
//    [Display(Name = "End Date")]
//    public DateTime? EndDate { get; set; }
//}