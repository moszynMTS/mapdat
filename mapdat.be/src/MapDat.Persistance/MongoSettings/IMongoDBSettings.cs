namespace MapDat.Persistance.MongoSettings
{
    public interface IMongoDBSettings
    {
        public string CollectionName { get; set; }
        public string MongoDbConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
