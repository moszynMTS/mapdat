using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MapDat.Domain.Entities
{
    public class GeoObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        [BsonElement("type")]
        public string Type { get; set; } = String.Empty;
        [BsonElement("features")]
        public FeaturesObject Features { get; set; } = null!;
        [BsonElement("properties")]
        public PropertiesObject Properties { get; set; } = null!;

    }
    public class PropertiesObject
    {
        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;
    }
    public class FeaturesObject
    {
        [BsonElement("type")]
        public string Type { get; set; } = String.Empty;
        [BsonElement("geometry")]
        public GeometryObject Geometry { get; set; } = null!;
    }
    public class GeometryObject
    {
        [BsonElement("type")]
        public string Type { get; set; } = String.Empty;
        [BsonElement("coordinates")]
        public string[][] Coordinates { get; set; } = null!;
    }
}
