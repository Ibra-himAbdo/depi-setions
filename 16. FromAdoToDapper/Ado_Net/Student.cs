using System;
using System.Collections.Generic;
using System.Text;

namespace Ado_Net
{
    public class Student
    {
        public int Id { get; set; }

        public int SID { get; set; }

        public string FNAME { get; set; } = string.Empty;

        public string LNAME { get; set; } = string.Empty;

        public DateTime BDATE { get; set; }

        public decimal? SALARY { get; set; }

        public string? CITY { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
