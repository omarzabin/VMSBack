namespace VMSBack.Src
{
    public class VehicleRegstration
    {
        public int RegId { get; set; }
        public int VehicleId { get; set; }
        public string VehicleClassification { get; set; }= string.Empty;
        public DateTime ExpiryDate { get; set; }

    }
}