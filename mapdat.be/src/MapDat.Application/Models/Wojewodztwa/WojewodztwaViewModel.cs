using MapDat.Domain.Common;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MapDat.Domain.Entities;

namespace MapDat.Application.Models.Wojewodztwa
{
    public class WojewodztwaViewModel
    {
        public WojewodztwaViewModel(WojewodztwoEntity entity)
        {
            Id = entity.Id;
            Type = entity.Type;
            Properties = new PropertiesObjectViewModel()
            {
                Name = entity.Properties.Name
            };
            Geometry = new GeometryObjectViewModel()
            {
                Type = entity.Geometry.Type,
                Coordinates = entity.Geometry.Coordinates[0].ToString()
            };
        }
        public string Id { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public PropertiesObjectViewModel Properties { get; set; } = null!;
        public GeometryObjectViewModel Geometry { get; set; } = null!;
        public class PropertiesObjectViewModel
        {
            public int Name { get; set; }
        }
        public class GeometryObjectViewModel
        {
            public string Type { get; set; } = String.Empty;
            public string Coordinates { get; set; } = null!;
        }
    }
}
