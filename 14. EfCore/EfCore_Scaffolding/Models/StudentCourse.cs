using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EfCore_Scaffolding.Models;

[PrimaryKey("Sid", "Cid")]
[Table("Student_Course")]
public partial class StudentCourse
{
    [Key]
    [Column("SID")]
    public int Sid { get; set; }

    [Key]
    [Column("CID")]
    public int Cid { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Grade { get; set; }

    [ForeignKey("Cid")]
    [InverseProperty("StudentCourses")]
    public virtual Course CidNavigation { get; set; } = null!;

    [ForeignKey("Sid")]
    [InverseProperty("StudentCourses")]
    public virtual Student SidNavigation { get; set; } = null!;
}
