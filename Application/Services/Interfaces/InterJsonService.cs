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
        void SaveItem(List<PlcsInUse> item);
        List<PlcsInUse?> LoadItem();
        void UpdateTag(int id, PlcsInUse updatedPlcConfigured);


        PlcSettings GetSettingsPlc();
        void UpdateSettingsPlc(PlcSettings settings);

    }
}
