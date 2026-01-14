using System.Security.Cryptography;

namespace EmployeeManagment.Services
{
    public class RefreshTokenService
    {
        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        }
    }
}
