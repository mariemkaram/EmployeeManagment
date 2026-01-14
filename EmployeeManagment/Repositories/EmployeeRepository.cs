using EmployeeManagment.Context;
using EmployeeManagment.Entities;
using EmployeeManagment.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees
                    .Include(e => e.Department)
                    .Include(e => e.JobTitle)
                    .ToList();

        }

        public Employee? GetByEmail(string email)
        {
            return _context.Employees
                       .Include(e => e.Department)
                       .Include(e => e.JobTitle)
                       .FirstOrDefault(e => e.Email == email);
        }

        public Employee? GetById(Guid id)
        {
            return _context.Employees
                        .Include(e => e.Department)
                        .Include(e => e.JobTitle)
                        .FirstOrDefault(e => e.Id == id);
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }
    }
}
