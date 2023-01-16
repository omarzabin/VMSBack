using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using System.Data.SqlClient;
using VMSBack.Src;

namespace VMSBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _config;

        public RegistrationController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpPost]

        public async Task<ActionResult<int>> Regester(VehicleOwner owner)
        {
            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var ownerId = await conn.ExecuteScalarAsync<int>
                (@"Insert into VehicleOwners (FirstName,LastName,Email,Password)
                 values(@FirstName,@LastName,@Email,@Password) select SCOPE_IDENTITY()"
                , new { owner.FirstName, owner.LastName, owner.Email, owner.Password });
            return Ok(ownerId);
        }

        [HttpGet ("{regId}")]

        public async Task<ActionResult<VehicleRegstration>> getRegstration (int regId)
        {
            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection3"));
            var reg = await conn.QueryAsync<VehicleRegstration>
                (@" select * from Registrations
                    WHERE RegId =@regId", new {regId});

            return Ok(reg);
        }
    }
}
