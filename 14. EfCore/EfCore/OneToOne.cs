//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace EfCore
//{
//    public class Employee
//    {
//        //[Key]
//        public int Id { get; set; }

//        //[Required, MaxLength(30)]
//        public string? Name { get; set; }

//        //[Column("Salaaaaaaaaaaaaaaaaaary")]
//        public decimal Salary { get; set; }

//        [NotMapped]
//        public DateTime TempDate { get; set; }
//        public DateTime CreateAt { get; set; }

//        public int CarId { get; set; } // fk
//        public Car? Car { get; set; }
//    }

//    public class Car
//    {
//        public int Id { get; set; }
//        public string? Model { get; set; }

//        public Employee? Employee { get; set; }
//    }
//}
