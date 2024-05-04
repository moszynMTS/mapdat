namespace MapDat.Persistance.MongoSettings
{
    public interface IMongoDBSettings
    {
        public string CollectionNames { get; set; }
        public string MongoDbConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
