using Domain.Models;

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
