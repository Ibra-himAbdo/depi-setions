using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EfCoreScaffolding.Models;

[Table("Instructor")]
public partial class Instructor
{
    [Key]
    [Column("InsID")]
    public int InsId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string InsName { get; set; } = null!;

    [Column(TypeName = "decimal(12, 2)")]
    public decimal? Bouns { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal? Salary { get; set; }

    [Column("Houre_Rate", TypeName = "decimal(12, 2)")]
    public decimal? HoureRate { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Address { get; set; }

    [Column("DID")]
    public int? Did { get; set; }

    [InverseProperty("Ins")]
    public virtual ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();

    [InverseProperty("Ins")]
    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    [ForeignKey("Did")]
    [InverseProperty("Instructors")]
    public virtual Department? DidNavigation { get; set; }
}
