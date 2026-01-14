using EmployeeManagment.Entities;

namespace EmployeeManagment.IRepositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(Guid id);
        Employee? GetByEmail(string email);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
    }
}
