
using MapDat.Domain.Entities;

namespace MapDat.Persistance.Services
{
    public interface IWojewodztwaService
    {
        Task<List<GeoObject>> GetWojewodztwa();
        GeoObject GetWojewodztwo(string id);
    }
}
