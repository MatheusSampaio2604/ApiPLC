﻿using Domain.Models;
using Domain.Repository.Interface;
using System.Text.Json;

namespace Domain.Repository
{
    /// <summary>
    /// In program.cs, this service is only for "PlcsInUse.Json"
    /// </summary>
    public class PlcsListRepositoy : IPlcListRepository
    {
        private readonly string _filePath;

        public PlcsListRepositoy(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(List<PlcsInUse> newItems)
        {
            List<PlcsInUse?> existingItems = Load() ?? [];

            foreach (var newItem in newItems)
            {
                PlcsInUse? existingItem = existingItems.FirstOrDefault(i => i.Id == newItem.Id);
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


        public List<PlcsInUse?> Load()
        {
            if (!File.Exists(_filePath))
                return default;

            string jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<PlcsInUse>>(jsonString);
        }

        public void Delete(int id)
        {
            List<PlcsInUse?> existingItems = Load() ?? [];

            PlcsInUse? itemToDelete = existingItems.FirstOrDefault(i => i.Id == id);
            if (itemToDelete != null)
            {
                existingItems.Remove(itemToDelete);

                string jsonString = JsonSerializer.Serialize(existingItems);
                File.WriteAllText(_filePath, jsonString);
            }
        }
    }
}
