namespace VMSBack.Src
{
    public class Events
    {
        public int VehicleID { get; set; }
        public int LocationId { get; set; }
        public int DeviceIMEI { get; set; }
        public DateTime GPSTime { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Speed { get; set; }
        public int Odometer { get; set; }
        public int VehicleIGN { get; set; }
        public int StreetSpeed { get; set; }
        public string ExtendedProperties { get; set; } = string.Empty;



    }
}
