using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Interface
{
    /// <summary>
    /// In program.cs, this interface service is only for "PlcsInUse.Json"
    /// </summary>
    public interface IPlcsJsonRepository
    {
        void Save(List<PlcsInUse> obj);
        List<PlcsInUse?> Load();

        void Delete(int id);
    }
}
