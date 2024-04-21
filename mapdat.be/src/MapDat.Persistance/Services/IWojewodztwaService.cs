
using MapDat.Domain.Entities;

namespace MapDat.Persistance.Services
{
    public interface IWojewodztwaService
    {
        Task<List<WojewodztwoEntity>> GetWojewodztwa();
        WojewodztwoEntity GetWojewodztwo(string id);
    }
}
