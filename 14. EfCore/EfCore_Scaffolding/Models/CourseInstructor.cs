using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EfCore_Scaffolding.Models;

[PrimaryKey("Cid", "InsId")]
[Table("Course_Instructor")]
public partial class CourseInstructor
{
    [Key]
    [Column("CID")]
    public int Cid { get; set; }

    [Key]
    [Column("InsID")]
    public int InsId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Evaluation { get; set; }

    [ForeignKey("Cid")]
    [InverseProperty("CourseInstructors")]
    public virtual Course CidNavigation { get; set; } = null!;

    [ForeignKey("InsId")]
    [InverseProperty("CourseInstructors")]
    public virtual Instructor Ins { get; set; } = null!;
}
