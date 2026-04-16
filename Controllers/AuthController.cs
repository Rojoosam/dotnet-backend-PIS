using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIADAL.Data;
using SIADAL.Services;
using SIADAL.Models.DTOs;

namespace SIADAL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwtService;

        public AuthController(AppDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.users
                .Include(u => u.student)
                .Include(u => u.teacher)
                .Include(u => u.role_users)
                    .ThenInclude(ru => ru.role)
                .FirstOrDefaultAsync(u => u.email == request.Email);

            if (user == null)
                return Unauthorized("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.password_hash))
                return Unauthorized("Invalid credentials");

            if (user.is_active != true)
                return Unauthorized("User inactive");

            // Determine role from role_user table, fallback to profile-based detection
            string role = "admin";

            var userRole = user.role_users.FirstOrDefault();
            if (userRole != null)
            {
                role = userRole.role.name;
            }
            else
            {
                if (user.student != null)
                    role = "student";
                else if (user.teacher != null)
                    role = "teacher";
            }

            var token = _jwtService.GenerateToken(user, role);

            return Ok(new
            {
                token,
                role,
                userId = user.id,
                expiration = DateTime.UtcNow.AddMinutes(60)
            });
        }
    }
}
