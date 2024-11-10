using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                return Ok(new { Message = "Login successful!", Role = foundUser.Role, Email = foundUser.Email, UserId = foundUser.user_id });
            }
            return Unauthorized(new { Message = "Invalid credentials." });
        }

        [HttpOptions("login")]
        public IActionResult Options()
        {
            return Ok(); // Respond to preflight request
        }

        // Endpoint to get the logged-in admin
        [HttpGet("get-admin-details")]
        public async Task<IActionResult> GetLoggedInAdmin([FromQuery] int userId)
        {
            if (userId <= 0) // Check if the userId is valid
            {
                return BadRequest("Invalid user ID.");
            }

            // Directly query the Admin table based on user_id
            var admin = await _context.Admins
                .Include(a => a.User) // Include related User data
                .FirstOrDefaultAsync(a => a.user_id == userId); // Filter by user_id

            if (admin == null)
            {
                return NotFound("Admin not found.");
            }

            return Ok(new
            {
                AdminId = admin.user_id,
                FirstName = admin.admin_fname,
                LastName = admin.admin_lname,
                Email = admin.User.Email, // assuming you want to include the User's email as well
                Role = admin.User.Role // assuming the User has a role property
            });
        }
    }
}
