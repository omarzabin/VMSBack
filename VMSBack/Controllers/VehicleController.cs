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
        public async Task<ActionResult<Vehicle>> GetVehicle(int vehicleId)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection2"));
            var vehicle = await connVMS.QueryAsync<Vehicle>(@"SELECT * FROM Vehicles WHERE VehicleId = @vehicleId",
                new { vehicleId });
            return Ok(vehicle); 

        }
        [HttpGet("getDeviceIMEI/")]
        public async Task<ActionResult<IEnumerable<string>>> GetDeviceIMEI(string ownerId)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection3"));
            var imei = await connVMS.QueryAsync<string>
                (@"use VMSDB
                    select top(1) v.DeviceIMEI 
                    from VehicleOwners VO
                    INNER JOIN VMSDB.dbo.Vehicles V ON V.DeviceIMEI = V.DeviceIMEI
                    where VO.OwnerId = @ownerId"
                     , new { ownerId });

            return Ok(imei);
        }

        [HttpPost]
        public async Task<ActionResult<int>> AddVehicle(Vehicle vehicle)
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
                    vehicle.RegId,
                    vehicle.InsId,
                    vehicle.DeviceIMEI
                });
         
            return Ok(vehicleId);
        }


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
