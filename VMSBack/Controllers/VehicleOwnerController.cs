using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using VMSBack.Src;

namespace VMSBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleOwnerController : ControllerBase
    {
        private readonly IConfiguration _config;
        public VehicleOwnerController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPut("{ownerId:int}")]
        public async Task<ActionResult<int>> UpdateOwnerInfo(VehicleOwner owner, int ownerId)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
         

            var ownerIdRes = await connVMS.ExecuteScalarAsync<int>
                (@"Update VehicleOwners SET FirstName =  @FirstName, LastName = @LastName, 
                    Email = @Email ,Password = @Password Where OwnerId = @ownerId select SCOPE_IDENTITY()",
                new
                {
                    owner.FirstName,
                    owner.LastName,
                    owner.Email,
                    owner.Password,
                    ownerId
                });

            return Ok(ownerIdRes);
        }

        [HttpGet]
        public async Task<ActionResult<Events>> SelectSecID()
        {
            using var connTrakingNDB = new SqlConnection(_config.GetConnectionString("DefaultConnection3"));

            //Genarate random Secound Id for the Vehicle from 460  to 500

            var events = await connTrakingNDB.QueryAsync<Events>("SELECT DISTINCT [VehicleID] FROM [dbo].[LastEvent]");

            return Ok(events);
        }

    }
}
