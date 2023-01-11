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
        public async Task<ActionResult<IEnumerable<Alerts>>> GetAlerts()
        {
            using var connVMS = new SqlConnection(_config.GetConnectionString("DefaultConnection3"));
            var registration = await connVMS.QueryAsync<Alerts>
                (@" select TOP 100 * from [AllEvents] where VehicleID = 460 order by GPSTime desc");

            return Ok(registration);
        }
    }

    public class Alerts
    {
        public int VehicleID { get; set; }
        public string TypeAr { get; set; }
        public string SubTypeAr { get; set; }
        public string ManufacturingYear { get; set; }
        public int LocationID { get; set; }
        public string DeviceIMEI { get; set; }
        public DateTime GPSTime { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Speed { get; set; }
        public bool VehicleIGN { get; set; }
        public double Odometer { get; set; }
        public string AddressAr { get; set; }
    }
}
