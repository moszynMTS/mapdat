using MapDat.Domain.Entities;
using MapDat.Application.Models.GeoObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace MapDat.Application.Models.Wojewodztwa
{
    public class WojewodztwaViewModel
    {
        public WojewodztwaViewModel(WojewodztwoEntity entity)
        {
            Id = entity.Id;
            Type = entity.Type;
            Properties = new PropertiesViewModel()
            {
                Name = entity.Properties.Name
            };
            Geometry = new GeometryViewModel()
            {
                Type = entity.Geometry.Type,
                Coordinates = ParseBisonArray(entity.Geometry.Coordinates)
            };
        }
        public string Id { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public PropertiesViewModel Properties { get; set; } = null!;
        public GeometryViewModel Geometry { get; set; } = null!;

        private List<string[]> ParseBisonArray(BsonArray coordinates)
        {
            BsonValue x = coordinates[0];
            List<string[]> test = new List<string[]>();

            foreach (var item in x.AsBsonArray)
            {
                var s = item.ToString();
                s = s.Replace(" ", "").Replace("[", "").Replace("]", "");
                test.Add(s.Split(','));
            }
            return test;
        }
    }
}