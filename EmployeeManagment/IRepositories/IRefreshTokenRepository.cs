using EmployeeManagment.Entities;

namespace EmployeeManagment.IRepositories
{
    public interface IRefreshTokenRepository
    {
        public void Add(RefreshToken entity);
        public void Update(RefreshToken entity);
        public void Delete(RefreshToken entity);
        public RefreshToken GetById(Guid Id);
        public RefreshToken GetByToken(string Id);
        public void SaveChanges();
    }
}
