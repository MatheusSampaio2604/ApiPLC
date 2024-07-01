using Application.Services.Interfaces;
using Domain.Models;
using Domain.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ConfigureOptionsService : InterConfigureOptionsService
    {
        private readonly IConfigureOptionsRepository _configureOptions;

        public ConfigureOptionsService(IConfigureOptionsRepository configureOptions)
        {
            _configureOptions = configureOptions;
        }

        public ConfigureOptionsWrapper? GetConfigureOptions()
        {
            return _configureOptions.GetConfigureOptions();
        }

        public List<CpuTypes>? GetCpuTypesOptions()
        {
            return _configureOptions.GetCpuTypesOptions();
        }

        public List<Drivers>? GetDriversOptions()
        {
            return _configureOptions.GetDriversOptions();
        }
    }
}
