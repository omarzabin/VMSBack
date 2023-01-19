using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using VMSBack.Src;

namespace VMSBack.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly IConfiguration _config;
        public InsuranceController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("{insId}")]
        public async Task<ActionResult<Insurance>> GetInsurance(int insId)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var insurance = await connVMS.QueryAsync<Insurance>
                (@" SELECT * FROM Insurance WHERE InsID = @insId ",
                new
                {
                    insId
                });

            return Ok(insurance);
        }

        [HttpPost]
        public async Task<ActionResult<Insurance>> AddInsurance(Insurance insurance)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var insId = await connVMS.ExecuteScalarAsync<int>
                (" INSERT INTO Insurance (InsuranceTy, ExpiryDate) "+
                "VALUES (@InsuranceTy,@ExpiryDate) select SCOPE_IDENTITY()",
                new
                {
                   
                    insurance.InsuranceTy,
                    insurance.ExpiryDate

                });
           
            return Ok(insId);
        }

        [HttpDelete("hardDelet/insId")]
        public async Task<ActionResult<int>> HardDelete(int insId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var res = await connection.ExecuteScalarAsync<int>
                (@"DELETE Insurance WHERE InsId = @insId",
                new { insId });
            return Ok(insId);
        }



    }
}
