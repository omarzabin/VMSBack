using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using VMSBack.Src;

namespace VMSBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IConfiguration _config;
        public VehicleController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet ("{vehicleId}")]
        public async Task<ActionResult<Vehicle>> GetVehiucle(int vehicleId)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var vehicle = await connVMS.QueryAsync<Vehicle>(@"SELECT * FROM Vehicles WHERE VehicleId = @vehicleId",
                new { vehicleId });
            return Ok(vehicle); 

        }

        [HttpPost("{regId:int},{insId:int}")]
        public async Task<ActionResult<Vehicle>> AddVehicle(Vehicle vehicle,int regId, int insId)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            //using var connTrakingNDB = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));

            var vehicleId = await connVMS.ExecuteScalarAsync<int>
                ("INSERT INTO Vehicles (VehicleModel,VehicleAutomaker,VehicleManufactureYear,VehiclePlateNumber,VehicleColor,RegId,InsId,DeviceIMEI)" +
                "VALUES (@VehicleModel, @VehicleAutomaker, @VehicleManufactureYear, @VehiclePlateNumber,@VehicleColor,@regId,@insId,@DeviceIMEI)" +
                "select SCOPE_IDENTITY()", 
                new{
                    vehicle.VehicleModel,
                    vehicle.VehicleAutomaker,
                    vehicle.VehicleManufactureYear,
                    vehicle.VehiclePlateNumber,
                    vehicle.VehicleColor,
                    regId,
                    insId,
                    vehicle.DeviceIMEI
                });
         
            return Ok(vehicleId);
        }

        //[HttpPut("{vehicleId:int}")]
        //public async Task<ActionResult<Vehicle>> UpdateVehicle(Vehicle vehicle,int vehicleId)
        //{
        //    using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
        //    //using var connTrakingNDB = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));

        //    var vehicleIdRes = await connVMS.ExecuteScalarAsync<int>
        //        (@"Update Vehicles SET VehicleType =  @VehicleType, VehicleAutomaker = @VehicleAutomaker, 
        //            VehicleManufactureYear = @VehicleManufactureYear ,VehiclePlateNumber = @VehiclePlateNumber ,VehicleColor,RegId,InsId)",
        //        new
        //        {
        //            vehicle.SecVehicleId,
        //            vehicle.VehicleType,
        //            vehicle.VehicleAutomaker,
        //            vehicle.VehicleManufactureYear,
        //            vehicle.VehiclePlateNumber,
        //            vehicle.VehicleColor,
        //            regId,
        //            insId
        //        });

        //    return Ok(vehicleIdRes);
        //}

        //[HttpGet]
        //public async Task<ActionResult<Events>> SelectSecID()
        //{
        //    using var connTrakingNDB = new SqlConnection(_config.GetConnectionString("DefaultConnection3"));

        //    //Genarate random Secound Id for the Vehicle from 460  to 500

        //    var events = await connTrakingNDB.QueryAsync<Events>("SELECT DISTINCT [VehicleID] FROM [dbo].[LastEvent]");

        //    return Ok(events);
        //}

        [HttpDelete("hardDelet/vehicleId")]
        public async Task<ActionResult<int>> HardDelete(int vehicleId)
        {
            using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var res = await connection.ExecuteScalarAsync<int>
                (@"DELETE Vehicles WHERE VehicleId = @vehicleId",
                new { vehicleId });
            return Ok(vehicleId);
        }
    }
}
