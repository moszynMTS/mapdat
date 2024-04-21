using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MapDat.Domain.Common
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
    }
}
