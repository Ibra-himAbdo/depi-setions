namespace FromAdoToDapper
{
    public class Student
    {
        public int Id { get; set; }
        public int SID { get; set; }
        public string? FNAME { get; set; }
        public string? LNAME { get; set; }
        public DateTime BDATE { get; set; }
        public decimal? SALARY { get; set; }
        public string? CITY { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, SID: {SID}, FNAME: {FNAME}, LNAME: {LNAME}, BDATE: {BDATE:yyyy-MM-dd}, SALARY: {SALARY}, CITY: {CITY}, CreatedAt: {CreatedAt:yyyy-MM-dd HH:mm:ss}";
        }
    }
}
