using EmployeeManagment.DTOs.User;
using EmployeeManagment.Entities;
using EmployeeManagment.Factory;
using EmployeeManagment.IRepositories;
using EmployeeManagment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeRepository _employeeRepo;
        private readonly JwtService _jwtService;
        private readonly RefreshTokenService _refreshTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepo;
        public AuthController(
            UserManager<ApplicationUser> userManager,
            IEmployeeRepository employeeRepo,
            JwtService jwtService,
            RefreshTokenService refreshTokenService,
            IRefreshTokenRepository refreshTokenRepository
            )
        {
            _userManager = userManager;
            _employeeRepo = employeeRepo;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
            _refreshTokenRepo = refreshTokenRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserDto dto)
        {

            var employee = _employeeRepo.GetByEmail(dto.Email);
            if (employee is null)
                return BadRequest("You are not registered as an employee");

            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser is not null)
                return Conflict("Email already registered");

            var user = new ApplicationUser
            {
               
                UserName = employee.FirstName + " "+employee.LastName,
                Email = dto.Email,

            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, "Employee");

            return Ok("User registered successfully");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null)
                return Unauthorized("Invalid email or password");

            var validPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (!validPassword)
                return Unauthorized("Invalid email or password");

            var roles = await _userManager.GetRolesAsync(user);

            // 5️⃣ generate tokens
            var accessToken = _jwtService.GenerateAccessToken(user, roles);

            var refreshTokenValue = _refreshTokenService.GenerateRefreshToken();
            var refreshToken = RefreshTokenFactory.Create(user.Id, refreshTokenValue, 7);

             _refreshTokenRepo.Add(refreshToken);
             _refreshTokenRepo.SaveChanges();

            // 6️⃣ response
            return Ok(new
            {
                accessToken,
                refreshToken = refreshToken.Token,
                user = new
                {
                    user.Id,
                    user.UserName,                 
                    user.Email,
                    Roles = roles
                }
            });
        }
    }
}


