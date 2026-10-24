using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EfCoreScaffolding.Models;

[Table("InstructorSalaryLog")]
public partial class InstructorSalaryLog
{
    [Key]
    [Column("LogID")]
    public int LogId { get; set; }

    [Column("InsID")]
    public int? InsId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? OldSalary { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? NewSalary { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
