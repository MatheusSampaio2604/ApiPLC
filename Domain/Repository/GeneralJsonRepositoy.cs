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
    public class GeneralJsonRepositoy : IGeneralJsonRepository
    {
        private readonly string _filePath;

        public GeneralJsonRepositoy(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(List<PlcConfigured> newItems)
        {
            // Carrega os itens existentes
            var existingItems = Load() ?? new List<PlcConfigured>();

            // Adiciona os novos itens à lista existente
            existingItems.AddRange(newItems);

            // Serializa a lista completa e grava no arquivo
            var jsonString = JsonSerializer.Serialize(existingItems);
            File.WriteAllText(_filePath, jsonString);
        }


        public List<PlcConfigured?> Load()
        {
            if(!File.Exists(_filePath))
                return default;

            var jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<PlcConfigured>>(jsonString);
        }

    }
}
