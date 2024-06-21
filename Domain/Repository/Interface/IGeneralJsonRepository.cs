using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Interface
{
    public interface IGeneralJsonRepository
    {
        void Save(List<PlcConfigured> obj);
        List<PlcConfigured?> Load();
    }
}
