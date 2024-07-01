using Domain.Models;
using Domain.Repository.Interface;
using System.Text.Json;

namespace Domain.Repository
{
    /// <summary>
    /// In program.cs, this service is only for "PlcSetting.Json"
    /// </summary>
    public class PlcSettingsRepository : IPlcSettingsRepository
    {
        private readonly string _filePath;

        public PlcSettingsRepository(string filePath)
        {
            _filePath = filePath;
        }

        public PlcSettings GetSettingsPlc()
        {
            if (!File.Exists(_filePath))
                return default;

            PlcSettingsWrapper? wrapper = JsonSerializer.Deserialize<PlcSettingsWrapper>(File.ReadAllText(_filePath));
            return wrapper.PlcSettings;
        }

        public void UpdateSettingsPlc(PlcSettings plcSettings)
        {
            File.WriteAllText(_filePath, JsonSerializer.Serialize(new PlcSettingsWrapper { PlcSettings = plcSettings }));
        }
    }
}
