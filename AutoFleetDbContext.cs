using Microsoft.EntityFrameworkCore;
using System;

namespace autofleetapi
{
    public class AutoFleetDbContext : DbContext
    {
        public AutoFleetDbContext(DbContextOptions<AutoFleetDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }


    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
