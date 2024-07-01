namespace Domain.Models
{
   
    public class PlcSettingsWrapper
    {
        public required PlcSettings PlcSettings { get; set; }
    }

    public class StartStopRequest
    {
        public required string AddressPlc { get; set; }
    }

    public class WriteRequest
    {
        public required string AddressPlc { get; set; }
        public required string Type { get; set; }
        public string? Value { get; set; }
    }

    public class ConfigureOptionsWrapper
    {
        public List<Drivers>? Drivers { get; set; }
        public List<CpuTypes>? CpuTypes { get; set; }
    }
}
