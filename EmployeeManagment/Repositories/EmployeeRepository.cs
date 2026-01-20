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

        public void GetEmployeeDept()
        {
            List<Emplist> emplists = (from  emp in _context.Employees
                                        join dept in _context.Departments on emp.DepartmentId equals dept.Id
                                        join jobTitle in _context.JobTitles on emp.JobTitleId equals jobTitle.Id
                                      select new Emplist()
                                      {
                                          DepartmentName = dept.Name,
                                          JobTitle = jobTitle.Title,
                                          EmployeeId = emp.Id.ToString(),
                                          EmployeeFirstName = emp.FirstName,
                                          EmployeeLastName = emp.LastName,
                                          Email = emp.Email,
                                          DepartmentId = emp.DepartmentId.ToString(),
                                          JobTitleId = emp.JobTitleId.ToString(),
                                      }).ToList();
        }
    }
    public class Emplist
    {
        public string DepartmentName { get; set; }
        public string JobTitle { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeFirstName { get; set; }
        public string EmployeeLastName { get; set; }
        public string Email { get; set; }
        public string DepartmentId { get; set; }
        public string JobTitleId { get; set; }
    }
}
