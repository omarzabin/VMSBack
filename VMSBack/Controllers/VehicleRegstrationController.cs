using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using VMSBack.Src;

namespace VMSBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleRegstrationController : ControllerBase
    {
        private readonly IConfiguration _config;
        public VehicleRegstrationController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("{regId}")]
        public async Task<ActionResult<VehicleRegstration>> GetInsurance(int regId)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var registration = await connVMS.QueryAsync<VehicleRegstration>
                (@" SELECT * FROM Registrations WHERE RegId = @regId ",
                new
                {
                    regId
                });

            return Ok(registration);
        }

        [HttpPost("AddRegstration")]
        public async Task<ActionResult<VehicleRegstration>> AddRegstration(VehicleRegstration regstration)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var regId = await connVMS.ExecuteScalarAsync<int>
                (@"Insert into Registrations (VehicleId,VehicleClassification,ExpiryDate)
                Values(@VehicleId,@VehicleClassification,@ExpiryDate) select SCOPE_IDENTITY()"
                , new {regstration.VehicleId, regstration.VehicleClassification, regstration.ExpiryDate });

            return Ok(regId);
        }

        [HttpPut("{regId:int}")]
        public async Task<ActionResult<VehicleRegstration>> UpdateVehicleRegstration(VehicleRegstration regstration, int regId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var regstrationId = await connection.ExecuteScalarAsync<int>
                (@"UPDATE Registrations SET VehicleClassification = @VehicleClassification, ExpiryDate = @ExpiryDate
                 WHERE RegId = @regId",new {regstration.VehicleClassification,regstration.ExpiryDate,regId});
            return Ok(regstrationId);
        }

        [HttpDelete("hardDelet/regId")]
        public async Task<ActionResult<int>> HardDelete(int regId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var res = await connection.ExecuteScalarAsync<int>
                (@"DELETE Registrations WHERE RegId = @regId",
                new { regId });
            return Ok(regId);
        }

    }
}
