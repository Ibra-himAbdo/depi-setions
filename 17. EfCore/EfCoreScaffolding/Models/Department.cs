using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EfCoreScaffolding.Models;

[Table("Department")]
public partial class Department
{
    [Key]
    [Column("DID")]
    public int Did { get; set; }

    [Column("DName")]
    [StringLength(100)]
    [Unicode(false)]
    public string Dname { get; set; } = null!;

    [Column("InsID")]
    public int? InsId { get; set; }

    public DateOnly? HireDate { get; set; }

    [ForeignKey("InsId")]
    [InverseProperty("Departments")]
    public virtual Instructor? Ins { get; set; }

    [InverseProperty("DidNavigation")]
    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    [InverseProperty("DidNavigation")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
