using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace autofleetapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AutoFleetDbContext _context;

        public UsersController(AutoFleetDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest(new { Message = "Invalid user data." });
            }

            var foundUser = _context.Users.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (foundUser != null)
            {
                return Ok(new { Message = "Login successful!", Role = foundUser.Role });
            }
            return Unauthorized(new { Message = "Invalid credentials." });
        }

        [HttpOptions("login")]
        public IActionResult Options()
        {
            return Ok(); // Respond to preflight request
        }
    }
}
