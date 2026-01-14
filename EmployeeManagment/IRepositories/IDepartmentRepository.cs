using EmployeeManagment.Entities;

namespace EmployeeManagment.IRepositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAllWithEmployees();
        IEnumerable<Department> GetAll();
        Department? GetById(Guid id);
        void Add(Department department);
        void Update(Department department);
        void Delete(Department department);
    }
}
