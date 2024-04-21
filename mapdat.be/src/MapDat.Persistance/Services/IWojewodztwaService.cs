
using MapDat.Domain.Entities;

namespace MapDat.Persistance.Services
{
    public interface IWojewodztwaService
    {
        Task<List<MyGeoObject>> GetWojewodztwa();
        MyGeoObject GetWojewodztwo(string id);
    }
}
