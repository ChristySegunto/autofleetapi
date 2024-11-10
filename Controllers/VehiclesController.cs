using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace autofleetapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly AutoFleetDbContext _context;
        public VehiclesController(AutoFleetDbContext context)
        {
            _context = context;
        }

        // GET: api/Vehicles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            try
            {
                var vehicles = await _context.Vehicles.ToListAsync();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/Vehicles/Count
        [HttpGet("Count")]
        public async Task<ActionResult<int>> GetTotalVehiclesCount()
        {
            try
            {
                // Get the count of total vehicles
                int totalCount = await _context.Vehicles.CountAsync();
                return Ok(totalCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/Vehicles/StatusCount
        [HttpGet("StatusCount")]
        public async Task<ActionResult<object>> GetVehiclesStatusCount()
        {
            try
            {
                // Count for "Available"
                int availableCount = await _context.Vehicles
                                                   .Where(v => v.vehicle_status == "Available")
                                                   .CountAsync();

                // Count for "Rented"
                int rentedCount = await _context.Vehicles
                                                 .Where(v => v.vehicle_status == "Rented")
                                                 .CountAsync();

                // Count for "Under Maintenance"
                int underMaintenanceCount = await _context.Vehicles
                                                          .Where(v => v.vehicle_status == "Under Maintenance")
                                                          .CountAsync();

                var statusCounts = new
                {
                    Available = availableCount,
                    Rented = rentedCount,
                    UnderMaintenance = underMaintenanceCount
                };

                return Ok(statusCounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // GET: api/Vehicles/CategoryCount
        [HttpGet("CategoryCount")]
        public async Task<ActionResult<object>> GetVehiclesCategoryCount()
        {
            try
            {
                // Count for "SUV"
                int suvCount = await _context.Vehicles
                                              .Where(v => v.vehicle_category == "SUV")
                                              .CountAsync();

                // Count for "Van"
                int vanCount = await _context.Vehicles
                                              .Where(v => v.vehicle_category == "Van")
                                              .CountAsync();

                // Count for "Sedan"
                int sedanCount = await _context.Vehicles
                                               .Where(v => v.vehicle_category == "Sedan")
                                               .CountAsync();

                var categoryCounts = new
                {
                    SUV = suvCount,
                    Van = vanCount,
                    Sedan = sedanCount
                };

                return Ok(categoryCounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
