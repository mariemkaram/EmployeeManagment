using EmployeeManagment.Context;
using EmployeeManagment.Entities;
using EmployeeManagment.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Department department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public void Delete(Department department)
        {
            _context.Departments.Remove(department);
            _context.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
             
        }

        public IEnumerable<Department> GetAllWithEmployees()
        {
            return _context.Departments
             .Include(d => d.Employees)
               .ThenInclude(e => e.JobTitle)
           .ToList();
        }

        public Department? GetById(Guid id)
        {
            return _context.Departments.Find(id);
        }

        public void Update(Department department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
        }
    }
}
