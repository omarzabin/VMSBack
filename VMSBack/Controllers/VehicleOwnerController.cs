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
                    Email = @Email ,Password = @Password , VehicleId=@VehicleId Where OwnerId = @ownerId",
                new
                {
                    owner.FirstName,
                    owner.LastName,
                    owner.Email,
                    owner.Password,
                    ownerId,
                    owner.VehicleId
                });

            return Ok(ownerIdRes);
        }
        [HttpPut]
        public async Task<ActionResult<int>> UpdateOwnerVehicleId(int ownerId,int vehicleId)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));

            var res = await connVMS.ExecuteScalarAsync<int>
                (@"Update VehicleOwners SET VehicleId = @vehicleId where OwnerId = @ownerId", new
                {
                    ownerId,
                    vehicleId
                });
            return Ok(res);


              
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
