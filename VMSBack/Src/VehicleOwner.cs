﻿namespace VMSBack.Src
{
    public class VehicleOwner
    {
        public int OwnerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int VehicleId { get; set; }
    }
}
