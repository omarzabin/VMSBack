namespace VMSBack.Src
{
    public class RepairRecords
    {
        public int RepId { get; set; }
        public int VehicleId { get; set; }
        public string WorkShop { get; set; } = string.Empty;
        public string Cost { get; set; } = string.Empty;

        public DateTime RepairDate { get; set; } 
    }
}
