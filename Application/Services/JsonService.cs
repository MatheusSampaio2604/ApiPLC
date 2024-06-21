using Application.Services.Interfaces;
using Domain.Models;
using Domain.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public  class JsonService : InterJsonService
    {
        private readonly IGeneralJsonRepository _jsonRepository;

        public JsonService(IGeneralJsonRepository jsonRepository)
        {
            _jsonRepository = jsonRepository;
        }

        public void SaveItem(List<PlcConfigured> item)
        {
            _jsonRepository.Save(item);
        }

        public List<PlcConfigured?> LoadItem()
        {
            return _jsonRepository.Load();
        }

    }
}
