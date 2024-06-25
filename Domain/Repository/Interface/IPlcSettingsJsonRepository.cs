using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Interface
{
    public interface IPlcSettingsJsonRepository
    {
        PlcSettings GetSettingsPlc();
        void UpdateSettingsPlc(PlcSettings plc);
    }
}
