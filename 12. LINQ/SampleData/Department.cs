namespace SampleData
{
    public class Department : BaseEntity
    {
        public new int Id { get; set; }
        public string? Name { get; set; }
        public int ManagerId { get; set; }

        public override string ToString()
        {
            return $"[Dept #{Id}] {Name,-25} (Manager ID: {ManagerId})";
        }
    }
}
