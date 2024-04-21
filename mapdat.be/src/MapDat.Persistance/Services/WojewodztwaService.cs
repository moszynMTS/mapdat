using MapDat.Persistance.MongoSettings;
using MapDat.Domain.Entities;
using MongoDB.Driver;

namespace MapDat.Persistance.Services
{
    public class WojewodztwaService : IWojewodztwaService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<WojewodztwoEntity> _wojewodztwa;
        public WojewodztwaService(IMongoDBSettings settings, IMongoClient mongoClient) 
        {
            _database = mongoClient.GetDatabase(settings.DatabaseName);
            _wojewodztwa = _database.GetCollection<WojewodztwoEntity>(settings.CollectionName);
        }
        public WojewodztwoEntity GetWojewodztwo(string id)
        {
            return _wojewodztwa.Find(x => x.Id==id).FirstOrDefault();
        }
        public async Task<List<WojewodztwoEntity>> GetWojewodztwa()
        {
            return await _wojewodztwa.Find(x => true).ToListAsync();
        }
    }
}
