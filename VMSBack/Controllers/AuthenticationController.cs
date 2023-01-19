using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using VMSBack.Src;
using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;


namespace VMSBack.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthenticationController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<VehicleOwner>> Login(VehicleOwnerDTO ownerDTO)
        {
            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var owner = await conn.QueryFirstOrDefaultAsync<VehicleOwner>
                 ("SELECT top(1)* FROM VehicleOwners where Email = @Email AND Password=@Password", new { ownerDTO.Email, ownerDTO.Password });
            return owner == null ? Unauthorized() : Ok(owner);

        }


    }
}
