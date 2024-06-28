using Application.Services.Interfaces;
using Domain.Models;
using Domain.Repository.Interface;

namespace Application.Services
{
    public class JsonService : InterJsonService
    {
        private readonly IPlcsJsonRepository _iPlcsRepository;
        private readonly IPlcSettingsJsonRepository _iPlcSettingsRepository;

        public JsonService(IPlcsJsonRepository iPlcsRepository, IPlcSettingsJsonRepository plcSettingsRepository)
        {
            _iPlcsRepository = iPlcsRepository;
            _iPlcSettingsRepository = plcSettingsRepository;
        }

        public void SaveItem(List<PlcsInUse> item)
        {
            _iPlcsRepository.Save(item);
        }

        public List<PlcsInUse?> LoadItem()
        {
            return _iPlcsRepository.Load();
        }

        public void UpdateTag(int id, PlcsInUse updatedPlcConfigured)
        {
            List<PlcsInUse?> items = _iPlcsRepository.Load();

            if (items == null)
                throw new Exception("Failed to load items from JSON file.");

            PlcsInUse? itemToUpdate = items.FirstOrDefault(i => i.Id == id);
            if (itemToUpdate != null)
            {
                itemToUpdate.Name = updatedPlcConfigured.Name;
                itemToUpdate.AddressPlc = updatedPlcConfigured.AddressPlc;
                itemToUpdate.Type = updatedPlcConfigured.Type;
                itemToUpdate.Value = updatedPlcConfigured.Value;

                _iPlcsRepository.Save(items);
            }
            else
            {
                throw new ArgumentException($"Item with ID {id} not found.");
            }
        }
        public PlcSettings GetSettingsPlc()
        {
            return _iPlcSettingsRepository.GetSettingsPlc();
        }

        public void UpdateSettingsPlc(PlcSettings settings)
        {

            _iPlcSettingsRepository.UpdateSettingsPlc(settings);
        }

        public void DeleteTagInList(int id)
        {
            _iPlcsRepository.Delete(id);
        }
    }
}
