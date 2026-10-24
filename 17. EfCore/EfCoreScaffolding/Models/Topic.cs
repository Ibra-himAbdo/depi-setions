using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EfCoreScaffolding.Models;

[Table("Topic")]
public partial class Topic
{
    [Key]
    [Column("TID")]
    public int Tid { get; set; }

    [Column("TName")]
    [StringLength(100)]
    [Unicode(false)]
    public string Tname { get; set; } = null!;

    [InverseProperty("TidNavigation")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
