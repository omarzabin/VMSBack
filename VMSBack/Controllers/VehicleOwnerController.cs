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
                    Email = @Email ,Password = @Password  Where OwnerId = @ownerId",
                new
                {
                    owner.FirstName,
                    owner.LastName,
                    owner.Email,
                    owner.Password,
                    ownerId,
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

       
    

    }
}
