using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace autofleetapi
{
    public class AutoFleetDbContext : DbContext
    {
        public AutoFleetDbContext(DbContextOptions<AutoFleetDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.user_id); // Set UserId as the primary key

            modelBuilder.Entity<Admin>()
                .ToTable("admin")
                .HasKey(a => a.admin_id);

            // Establish the foreign key relationship between User and Admin
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.User)  // Admin has one User
                .WithOne()            // One User corresponds to one Admin (assuming one-to-one relationship)
                .HasForeignKey<Admin>(a => a.user_id); // Admin's user_id is the foreign key referencing User's user_id


            //VEHICLE
            // Vehicle mapping
            modelBuilder.Entity<Vehicle>()
                .ToTable("vehicle")
                .HasKey(v => v.vehicle_id);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.car_manufacturer)
                .HasMaxLength(50)
                .IsRequired();

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.car_model)
                .HasMaxLength(50)
                .IsRequired();
            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => v.plate_number)   // Create an index for plate_number
                .IsUnique();
            modelBuilder.Entity<Vehicle>()
                .Property(v => v.plate_number)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.manufacture_year)
                .IsRequired(false);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.vehicle_color)
                .HasMaxLength(30);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.fuel_type)
                .HasMaxLength(20);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.transmission_type)
                .HasMaxLength(20);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.seating_capacity)
                .IsRequired(false);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.vehicle_category)
                .HasMaxLength(50);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.total_mileage)
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValue(0m);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.total_fuel_consumption)
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValue(0m);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.distance_traveled)
                .HasColumnType("decimal(10, 2)")
                .HasDefaultValue(0m);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.vehicle_status)
                .HasMaxLength(20);

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.created_at)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Vehicle>()
                .Property(v => v.updated_at)
                .HasDefaultValueSql("GETDATE()");
        }

    }

    [Table("Users")]

    public class User
    {
        public int user_id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    [Table("admin")]
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

    [Table("vehicle")]
    public class Vehicle
    {
        public int vehicle_id { get; set; }
        public string car_manufacturer { get; set; }
        public string car_model { get; set; }
        public string plate_number { get; set; }
        public int? manufacture_year { get; set; }
        public string vehicle_color { get; set; }
        public string fuel_type { get; set; }
        public string transmission_type { get; set; }
        public int? seating_capacity { get; set; }
        public string vehicle_category { get; set; }
        public decimal total_mileage { get; set; }
        public decimal total_fuel_consumption { get; set; }
        public decimal distance_traveled { get; set; }
        public string vehicle_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
