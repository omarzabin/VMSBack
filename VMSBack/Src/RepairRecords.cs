namespace VMSBack.Src
{
    public class RepairRecords
    {
        public int RepairId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string WorkShop { get; set; } = string.Empty;
        public int OilLife { get; set; }
        public int VehicleId { get; set; }
        public DateTime RepairDate { get; set; } 
    }
}
