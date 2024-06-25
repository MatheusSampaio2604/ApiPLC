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
    public class PlcsJsonRepositoy : IPlcsJsonRepository
    {
        private readonly string _filePath;

        public PlcsJsonRepositoy(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(List<PlcConfigured> newItems)
        {
            List<PlcConfigured?> existingItems = Load() ?? [];

            foreach (var newItem in newItems)
            {
                PlcConfigured? existingItem = existingItems.FirstOrDefault(i => i.Id == newItem.Id);
                if (existingItem != null)
                {
                    existingItem.Name = newItem.Name;
                    existingItem.AddressPlc = newItem.AddressPlc;
                    existingItem.Type = newItem.Type;
                    existingItem.Value = newItem.Value;
                }
                else
                {
                    int lastId = existingItems.Count > 0 ? existingItems.Max(i => i.Id) : 0;
                    newItem.Id = ++lastId;
                    existingItems.Add(newItem);
                }
            }

            string jsonString = JsonSerializer.Serialize(existingItems);
            File.WriteAllText(_filePath, jsonString);
        }


        public List<PlcConfigured?> Load()
        {
            if(!File.Exists(_filePath))
                return default;

            string jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<PlcConfigured>>(jsonString);
        }
    }
}
