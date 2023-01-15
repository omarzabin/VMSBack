using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using VMSBack.Src;

namespace VMSBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AlertsController(IConfiguration configuration)
        {
            _config = configuration;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alerts>>> GetAlerts(string IMEI)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection3"));
            var registration = await connVMS.QueryAsync<Alerts>
                (@"use TrackingNDB 
                    select top(30) AE.AddressAr,AE.DeviceIMEI,AE.ExtendedProperties,AE.GPSTime,AE.Latitude,AE.Longitude,AE.Odometer,AE.Speed,AE.StreetSpeed,AE.VehicleIGN,AE.LocationID
                    from AllEvents AE
                    INNER JOIN VMSDB.dbo.Vehicles V ON V.DeviceIMEI = AE.DeviceIMEI 
                    where V.DeviceIMEI = @IMEI", new { IMEI });

            return Ok(registration);
        }
        [HttpGet("latest/")]
        public async Task<ActionResult<IEnumerable<Alerts>>> GetAlertLatest(string IMEI)
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection3"));
            var registration = await connVMS.QueryAsync<Alerts>
                (@"use TrackingNDB 
                    select top(1) AE.AddressAr,AE.DeviceIMEI,AE.ExtendedProperties,AE.GPSTime,AE.Latitude,AE.Longitude,AE.Odometer,AE.Speed,AE.StreetSpeed,AE.VehicleIGN,AE.LocationID
                    from AllEvents AE
                    INNER JOIN VMSDB.dbo.Vehicles V ON V.DeviceIMEI = AE.DeviceIMEI 
                    where V.DeviceIMEI = @IMEI
                    ORDER BY AE.GPSTime DESC", new { IMEI });

            return Ok(registration);
        }
    }

}