using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace autofleetapi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle",
                columns: table => new
                {
                    vehicle_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    car_manufacturer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    car_model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    plate_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    manufacture_year = table.Column<int>(type: "int", nullable: true),
                    vehicle_color = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    fuel_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    transmission_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    seating_capacity = table.Column<int>(type: "int", nullable: true),
                    vehicle_category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    total_mileage = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    total_fuel_consumption = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    distance_traveled = table.Column<decimal>(type: "decimal(10,2)", nullable: false, defaultValue: 0m),
                    vehicle_status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle", x => x.vehicle_id);
                });

            migrationBuilder.CreateTable(
                name: "admin",
                columns: table => new
                {
                    admin_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    admin_fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    admin_mname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    admin_lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    admin_birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin", x => x.admin_id);
                    table.ForeignKey(
                        name: "FK_admin_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_admin_user_id",
                table: "admin",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_plate_number",
                table: "vehicle",
                column: "plate_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin");

            migrationBuilder.DropTable(
                name: "vehicle");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
