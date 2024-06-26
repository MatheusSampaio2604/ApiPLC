﻿using Domain.Models;

namespace Domain.Repository.Interface
{
    /// <summary>
    /// In program.cs, this interface service is only for "PlcSetting.Json"
    /// </summary>
    public interface IPlcSettingsRepository
    {
        PlcSettings GetSettingsPlc();
        void UpdateSettingsPlc(PlcSettings plc);
    }
}
