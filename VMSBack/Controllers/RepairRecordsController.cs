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
            var records= await connVMS.QueryAsync<RepairRecords>(@"SELECT * FROM RepairRecords WHERE VehicleId = @vehicleId",
                new { vehicleId });
            return Ok(records);

        }
        [HttpPost]
        public async Task<ActionResult<int>> AddRecord(RepairRecords record)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            //using var connTrakingNDB = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));


            var repId = await connVMS.ExecuteScalarAsync<int>
                (@"INSERT INTO RepairRecords (PartName,Description,Price,WorkShop,OilLife,VehicleId,RepairDate)
                VALUES (@PartName,@Description,@Price,@WorkShop,@OilLife,@VehicleId,@RepairDate) 
                select SCOPE_IDENTITY()",
                new
                {
                   record.PartName,
                   record.Description,
                   record.Price,
                   record.WorkShop,
                   record.OilLife,
                   record.VehicleId,
                   record.RepairDate
                });

            return Ok(repId);
        }

    }
}
