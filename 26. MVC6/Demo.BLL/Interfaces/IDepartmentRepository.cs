namespace Demo.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<int> AddAsync(Department department);
        Task<int> DeleteAsync(Department department);
        Task<int> DeleteAsync(int id);
        Task<int> EditAsync(Department department);
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
    }
}