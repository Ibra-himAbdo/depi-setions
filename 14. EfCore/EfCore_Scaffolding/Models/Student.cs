using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EfCore_Scaffolding.Models;

[Table("Student")]
[Index("Fname", Name = "IX_Student_FName")]
[Index("Fname", "Lname", Name = "IX_Student_Name")]
public partial class Student
{
    [Key]
    [Column("SID")]
    public int Sid { get; set; }

    [Column("FName")]
    [StringLength(50)]
    [Unicode(false)]
    public string Fname { get; set; } = null!;

    [Column("LName")]
    [StringLength(50)]
    [Unicode(false)]
    public string Lname { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Address { get; set; }

    public int? Age { get; set; }

    [Column("DID")]
    public int? Did { get; set; }

    [Column("SuperID")]
    public int? SuperId { get; set; }

    [ForeignKey("Did")]
    [InverseProperty("Students")]
    public virtual Department? DidNavigation { get; set; }

    [InverseProperty("Super")]
    public virtual ICollection<Student> InverseSuper { get; set; } = new List<Student>();

    [InverseProperty("SidNavigation")]
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    [ForeignKey("SuperId")]
    [InverseProperty("InverseSuper")]
    public virtual Student? Super { get; set; }
}
