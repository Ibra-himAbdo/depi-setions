using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EfCore_Scaffolding.Models;

[Table("Course")]
public partial class Course
{
    [Key]
    [Column("CID")]
    public int Cid { get; set; }

    [Column("CName")]
    [StringLength(100)]
    [Unicode(false)]
    public string Cname { get; set; } = null!;

    public int? Duration { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? Desc { get; set; }

    [Column("TID")]
    public int? Tid { get; set; }

    [InverseProperty("CidNavigation")]
    public virtual ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();

    [InverseProperty("CidNavigation")]
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    [ForeignKey("Tid")]
    [InverseProperty("Courses")]
    public virtual Topic? TidNavigation { get; set; }
}
