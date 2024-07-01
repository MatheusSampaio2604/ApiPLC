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
    public class ConfigureOptionsRepository : IConfigureOptionsRepository
    {
        private readonly string _filePath;

        public ConfigureOptionsRepository(string filePath)
        {
            _filePath = filePath;
        }

        public ConfigureOptionsWrapper? GetConfigureOptions()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<ConfigureOptionsWrapper>(json);
        }

        public List<Drivers>? GetDriversOptions()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<ConfigureOptionsWrapper>(json).Drivers;
        }

        public List<CpuTypes>? GetCpuTypesOptions()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<ConfigureOptionsWrapper>(json).CpuTypes;
        }
    }
}