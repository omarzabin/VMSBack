using System.Numerics;

namespace VMSBack.Src
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        public string VehicleModel { get; set;} = string.Empty;
        public string VehicleAutomaker { get; set; } = string.Empty;
        public int VehicleManufactureYear { get; set; }
        public string VehiclePlateNumber { get; set; }= string.Empty;
        public string VehicleColor { get; set; } = string.Empty;
        public int RegId { get; set; } 
        public int InsId{ get; set; }
        public BigInteger DeviceIMEI { get; set; }

    }
}
