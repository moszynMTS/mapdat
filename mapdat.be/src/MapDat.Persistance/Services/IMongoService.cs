
using MapDat.Domain.Entities;
using MongoDB.Driver;

namespace MapDat.Persistance.Services
{
    public interface IMongoService
    {
        public  IMongoCollection<GminaEntity>getGmina();
        public WojewodztwoEntity GetWojewodztwo(string id);
        public Task<List<WojewodztwoEntity>> GetWojewodztwa();
        public PowiatEntity GetPowiat(string id);
        public Task<List<PowiatEntity>> GetPowiaty(string wojewodztwo);
        public Task UpdatePowiaty(string[] powiaty, string woj);
        public GminaEntity GetGmina(string id);
        public Task<List<GminaEntity>> GetGminy(string powiat, string powiatId);
        public InfoEntity GetInfo(string wojewodztwoId);
    }
}
