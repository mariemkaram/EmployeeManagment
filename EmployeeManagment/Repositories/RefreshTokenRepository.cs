using EmployeeManagment.Context;
using EmployeeManagment.Entities;
using EmployeeManagment.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

       
        public void Add(RefreshToken entity)
        {
            _context.RefreshTokens.Add(entity);
        }

        public void Update(RefreshToken entity)
        {
            _context.RefreshTokens.Update(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public RefreshToken GetByToken(string token)
        {
            return _context.RefreshTokens
                .Include(t => t.User)
                .FirstOrDefault(t => t.Token == token);
        }

        public void Delete(RefreshToken entity)
        {
            _context.RefreshTokens.Remove(entity);
        }

        public RefreshToken GetById(Guid id)
        {
            return _context.RefreshTokens
                .FirstOrDefault(t => t.Id == id);
        }
    }
}
