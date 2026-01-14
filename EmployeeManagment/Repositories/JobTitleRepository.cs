using EmployeeManagment.Context;
using EmployeeManagment.Entities;
using EmployeeManagment.IRepositories;

namespace EmployeeManagment.Repositories
{
    public class JobTitleRepository : IJobTitleRepository
    {
        private readonly AppDbContext _context;

        public JobTitleRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(JobTitle jobTitle)
        {
            _context.JobTitles.Add(jobTitle);
            _context.SaveChanges();
        }

        public void Delete(JobTitle jobTitle)
        {
            _context.JobTitles.Remove(jobTitle);
            _context.SaveChanges();
        }

        public IEnumerable<JobTitle> GetAll()
        {
            return _context.JobTitles.ToList();
        }

        public JobTitle? GetById(Guid id)
        {
            return _context.JobTitles.Find(id);
        }

        public void Update(JobTitle jobTitle)
        {
            _context.JobTitles.Update(jobTitle);
            _context.SaveChanges();
        }
    }
}
