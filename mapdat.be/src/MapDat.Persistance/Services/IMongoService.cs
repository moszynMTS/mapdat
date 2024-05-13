
using MapDat.Domain.Entities;

namespace MapDat.Persistance.Services
{
    public interface IMongoService
    {
        public WojewodztwoEntity GetWojewodztwo(string id);
        public Task<List<WojewodztwoEntity>> GetWojewodztwa();
        public PowiatEntity GetPowiat(string id);
        public Task<List<PowiatEntity>> GetPowiaty(string wojewodztwo);
        public Task UpdatePowiaty(string[] powiaty, string woj);
        public GminaEntity GetGmina(string id);
        public Task<List<GminaEntity>> GetGminy(string powiat);
    }
}
