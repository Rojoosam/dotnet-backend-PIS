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
            //var pwd = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = await _context.users
                .Include(u => u.students)
                .Include(u => u.teachers)
                .FirstOrDefaultAsync(u => u.email == request.Email);

            if (user == null)
                return Unauthorized("Invalid credentials");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.password))
                return Unauthorized("Invalid credentials");

            if (user.is_active != true)
                return Unauthorized("User inactive");

            string role = "admin";

            if (user.students.Any())
                role = "student";
            else if (user.teachers.Any())
                role = "teacher";

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
