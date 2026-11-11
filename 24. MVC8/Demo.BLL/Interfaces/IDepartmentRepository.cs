namespace Demo.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<int> AddAsync(Department department);
        Task<int> DeleteAsync(Department department);
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task<int> UpdateAsync(Department department);
    }
}