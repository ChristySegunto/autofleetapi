using Microsoft.EntityFrameworkCore;
using System;

namespace autofleetapi
{
    public class AutoFleetDbContext : DbContext
    {
        public AutoFleetDbContext(DbContextOptions<AutoFleetDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.user_id); // Set UserId as the primary key
        }
    }


    public class User
    {
        public int user_id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
