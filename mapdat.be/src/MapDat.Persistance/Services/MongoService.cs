using MapDat.Persistance.MongoSettings;
using MapDat.Domain.Entities;
using MongoDB.Driver;

namespace MapDat.Persistance.Services
{
    public class MongoService : IMongoService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<WojewodztwoEntity> _wojewodztwa;
        private readonly IMongoCollection<PowiatEntity> _powiaty;
        private readonly IMongoCollection<GminaEntity> _gminy;
        public MongoService(IMongoDBSettings settings, IMongoClient mongoClient) 
        {
            var CollectionNamesList = settings.CollectionNames.Split(';').ToList();
            _database = mongoClient.GetDatabase(settings.DatabaseName);
            _wojewodztwa = _database.GetCollection<WojewodztwoEntity>(CollectionNamesList[0]);
            _powiaty = _database.GetCollection<PowiatEntity>(CollectionNamesList[1]);
            _gminy = _database.GetCollection<GminaEntity>(CollectionNamesList[2]);
        }
        #region GeoObjectsGets
        public WojewodztwoEntity GetWojewodztwo(string id)
        {
            return _wojewodztwa.Find(x => x.Id==id).FirstOrDefault();
        }
        public async Task<List<WojewodztwoEntity>> GetWojewodztwa()
        {
            return await _wojewodztwa.Find(x => true).ToListAsync();
        }
        public PowiatEntity GetPowiat(string id)
        {
            return _powiaty.Find(x => x.Id == id).FirstOrDefault();
        }
        public async Task<List<PowiatEntity>> GetPowiaty(string wojewodztwo)
        {
            return await _powiaty.Find(x => x.Properties.Wojewodztwo== wojewodztwo).ToListAsync();
        }
        public GminaEntity GetGmina(string id)
        {
            return _gminy.Find(x => x.Id == id).FirstOrDefault();
        }
        public async Task<List<GminaEntity>> GetGminy()
        {
            return await _gminy.Find(x => true).ToListAsync();
        }
        #endregion
    }
}
