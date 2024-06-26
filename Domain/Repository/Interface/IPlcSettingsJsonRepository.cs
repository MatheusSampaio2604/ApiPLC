using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Interface
{
    /// <summary>
    /// In program.cs, this interface service is only for "PlcSetting.Json"
    /// </summary>
    public interface IPlcSettingsJsonRepository
    {
        PlcSettings GetSettingsPlc();
        void UpdateSettingsPlc(PlcSettings plc);
    }
}
