using MongoDB.Bson.Serialization.Attributes;

namespace MapDat.Domain.Entities
{
    public class GminaEntity : MyGeoObject<GminaPropertiesObject>
    {
    }
    public class GminaPropertiesObject : PropertiesObject
    {
        [BsonElement("powiat")]
        public string Powiat { get; set; } = null!;
    }
}
