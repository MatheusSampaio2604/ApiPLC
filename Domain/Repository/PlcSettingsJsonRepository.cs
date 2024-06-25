using Domain.Models;
using Domain.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class PlcSettingsJsonRepository : IPlcSettingsJsonRepository
    {
        private readonly string _filePath;

        public PlcSettingsJsonRepository(string filePath)
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
