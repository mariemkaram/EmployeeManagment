using EmployeeManagment.Entities;

namespace EmployeeManagment.IRepositories
{
    public interface IJobTitleRepository
    {
        IEnumerable<JobTitle> GetAll();
        JobTitle? GetById(Guid id);
        void Add(JobTitle jobTitle);
        void Update(JobTitle jobTitle);
        void Delete(JobTitle jobTitle);
    }
}
