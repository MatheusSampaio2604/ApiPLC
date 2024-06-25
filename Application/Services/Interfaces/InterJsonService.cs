using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface InterJsonService
    {
        void SaveItem(List<PlcConfigured> item);
        List<PlcConfigured?> LoadItem();
        void UpdateTag(int id, PlcConfigured updatedPlcConfigured);


        PlcSettings GetSettingsPlc();
        void UpdateSettingsPlc(PlcSettings settings);

    }
}
