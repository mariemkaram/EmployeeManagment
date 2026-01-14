using EmployeeManagment.Entities;

namespace EmployeeManagment.Factory
{
    public static class RefreshTokenFactory
    {
        public static RefreshToken Create(Guid userId, string Token, int expiryDayes)
        {
            return new RefreshToken
            {
                Token = Token,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(expiryDayes),
            };

        }
    }
}
