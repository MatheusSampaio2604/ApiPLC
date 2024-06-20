﻿namespace Domain.Models
{
    public class PlcSettings
    {
        public required string Ip1 {  get; set; }
        public required string CpuType { get; set; }
        public required short Rack { get; set; }
        public required short Slot { get; set; }
    }

    public class StartStopRequest
    {
        public required string AddressPlc { get; set; }
    }

    public class WriteRequest
    {
        public required string AddressPlc { get; set; }
        public required string Type { get; set; }
        public object? Value { get; set; }
    }
}