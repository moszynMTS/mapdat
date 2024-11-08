﻿using MapDat.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MapDat.Domain.Entities
{
    public class MyGeoObject<TProperties> : BaseEntity 
        where TProperties : PropertiesObject
    {
        [BsonElement("type")]
        public string Type { get; set; } = String.Empty;
        [BsonElement("properties")]
        public TProperties Properties { get; set; } = null!;
        [BsonElement("geometry")]
        public GeometryObject Geometry { get; set; } = null!;
    }
    public class PropertiesObject
    {
        [BsonElement("nazwa")]
        public string Name { get; set; } = null!;
    }
    public class GeometryObject
    {
        [BsonElement("type")]
        public string Type { get; set; } = String.Empty;
        [BsonElement("coordinates")]
        public BsonArray[] Coordinates { get; set; } = null!;
    }
}
