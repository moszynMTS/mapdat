namespace MapDat.Persistance.MongoSettings
{
    public class MongoDBSettings : IMongoDBSettings
    {
        public string CollectionName { get; set; } = string.Empty;
        public string MongoDbConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
