using Domain.Models;

namespace Application.Services.Interfaces
{
    public interface InterJsonService
    {
        void SaveItem(List<PlcsInUse> item);
        List<PlcsInUse?> LoadItem();
        void UpdateTag(int id, PlcsInUse updatedPlcConfigured);
        void DeleteTagInList(int id);


        PlcSettings GetSettingsPlc();
        void UpdateSettingsPlc(PlcSettings settings);

    }
}
