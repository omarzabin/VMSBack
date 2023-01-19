using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using VMSBack.Src;

namespace VMSBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairRecordsController : ControllerBase
    {
        private readonly IConfiguration _config;
        public RepairRecordsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("{vehicleId}")]
        public async Task<ActionResult<RepairRecords>> GetRecords(int vehicleId)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var records= await connVMS.QueryAsync<RepairRecords>(@"SELECT * FROM Vehicles WHERE VehicleId = @vehicleId",
                new { vehicleId });
            return Ok(records);

        }
        [HttpPost]
        public async Task<ActionResult<int>> AddRecords(RepairRecords records)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            //using var connTrakingNDB = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));

            var vehicleId = await connVMS.ExecuteScalarAsync<int>
                ("INSERT INTO Vehicles ()" +
                "VALUES ()" +
                "select SCOPE_IDENTITY()",
                new
                {
                    
                });

            return Ok(vehicleId);
        }

    }
}
