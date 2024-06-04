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
        public readonly IMongoCollection<GminaEntity> _gminy;
        public MongoService(IMongoDBSettings settings, IMongoClient mongoClient) 
        {
            var CollectionNamesList = settings.CollectionNames.Split(';').ToList();
            _database = mongoClient.GetDatabase(settings.DatabaseName);
            _wojewodztwa = _database.GetCollection<WojewodztwoEntity>(CollectionNamesList[0]);
            _powiaty = _database.GetCollection<PowiatEntity>(CollectionNamesList[1]);
            _gminy = _database.GetCollection<GminaEntity>(CollectionNamesList[2]);
        }
        #region GeoObjectsGets
        public IMongoCollection<GminaEntity> getGmina()
        {
            return _gminy;
        }
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
        public async Task UpdatePowiaty(string[] powiaty, string woj)
        {
            var filter = Builders<PowiatEntity>.Filter.In(x => x.Properties.Name, powiaty);
            var update = Builders<PowiatEntity>.Update.Set(x => x.Properties.Wojewodztwo, woj);

            await _powiaty.UpdateManyAsync(filter, update);
        }
        public async Task<List<PowiatEntity>> GetPowiaty(string wojewodztwo)
        {
            return await _powiaty.Find(x => x.Properties.Wojewodztwo.ToLower() == wojewodztwo.ToLower()).ToListAsync();
        }
        public GminaEntity GetGmina(string id)
        {
            return _gminy.Find(x => x.Id == id).FirstOrDefault();
        }
        public async Task<List<GminaEntity>> GetGminy(string powiat, string powiatId)
        {
            var byId = await _gminy.Find(x => x.Properties.PowiatId == powiatId
                                        ).ToListAsync();
            if (byId.Count > 0)
                return byId;
            return await _gminy.Find(x => x.Properties.Powiat.ToLower() == powiat.ToLower() && x.Properties.PowiatId==null
                                        ).ToListAsync();
        }
        #endregion
    }
}
