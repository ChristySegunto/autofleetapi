using Microsoft.EntityFrameworkCore;
using System;

namespace autofleetapi
{
    public class AutoFleetDbContext : DbContext
    {
        public AutoFleetDbContext(DbContextOptions<AutoFleetDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.user_id); // Set UserId as the primary key

            modelBuilder.Entity<Admin>()
                .HasKey(a => a.admin_id);

            // Establish the foreign key relationship between User and Admin
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)  // Admin has one User
                .WithOne()            // One User corresponds to one Admin (assuming one-to-one relationship)
                .HasForeignKey<Admin>(a => a.user_id); // Admin's user_id is the foreign key referencing User's user_id
        }
    }


    public class User
    {
        public int user_id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class Admin
    {
        public int admin_id { get; set; }
        public int user_id { get; set; } // Foreign key to User table
        public string admin_fname { get; set; }
        public string admin_mname { get; set; }
        public string admin_lname { get; set; }
        public DateTime? admin_birthday { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        // Navigation property to the associated User
        public User User { get; set; }
    }
}
