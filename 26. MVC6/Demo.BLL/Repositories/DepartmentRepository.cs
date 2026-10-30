using Demo.BLL.Interfaces;
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


        public List<Department> GetAll()
        {
            return _context.Departments.AsNoTracking().ToList();
        }

        public Department? GetById(int id)
        {
           return _context.Departments.Find(id);
        }

        public int Add(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();
        }

        public int Edit(Department department)
        {
            _context.Departments.Update(department);
            return _context.SaveChanges();
        }

        public int Delete(Department department)
        {
            _context.Departments.Remove(department);
            return _context.SaveChanges();
        }

        //public int Delete(int id)
        //{
        //    _context.Departments.Where(d => d.Id == id).ExecuteDelete();
        //}
    }
}