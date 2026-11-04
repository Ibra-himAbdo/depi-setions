namespace Demo.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        List<Department> GetAll();
        Department? GetById(int id);
        int Add(Department department);
        int Update(Department department);
        int Delete(Department department);
    }
}
