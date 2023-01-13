namespace VMSBack.Src
{

    public class Alerts
    {
   
        public string AddressAr { get; set; } =string.Empty;
        public string DeviceIMEI { get; set; } = string.Empty;
        public string ExtendedProperties { get; set; } = string.Empty;  
        public DateTime GPSTime { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Odometer { get; set; }
        public int Speed { get; set; }
        public int StreetSpeed { get; set; }
        public bool VehicleIGN { get; set; }


    }
 


}
