using Demo.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }


        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.AsNoTracking().ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task<int> AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> EditAsync(Department department)
        {
            _context.Departments.Update(department);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Department department)
        {
            _context.Departments.Remove(department);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _context.Departments.Where(d => d.Id == id).ExecuteDeleteAsync();
        }
    }
}