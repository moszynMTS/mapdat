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
        [BsonElement("properties")]
        public PropertiesObject Properties { get; set; } = null!;
        [BsonElement("geometry")]
        public GeometryObject Geometry { get; set; } = null!;
    }
    public class PropertiesObject
    {
        [BsonElement("Nazwa")]
        public int Name { get; set; }
    }
    public class GeometryObject
    {
        [BsonElement("type")]
        public string Type { get; set; } = String.Empty;
        [BsonElement("coordinates")]
        public BsonArray[] Coordinates { get; set; } = null!;
    }
}
