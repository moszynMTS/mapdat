using MongoDB.Bson.Serialization.Attributes;

namespace MapDat.Domain.Entities
{
    public class PowiatEntity : MyGeoObject<PowiatPropertiesObject>
    {
    }
    public class PowiatPropertiesObject : PropertiesObject
    {
        [BsonElement("wojewodztwo")]
        public string Wojewodztwo { get; set; } = null!;
    }
}
