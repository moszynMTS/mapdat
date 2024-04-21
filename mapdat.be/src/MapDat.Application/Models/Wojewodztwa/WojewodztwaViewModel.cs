using MapDat.Domain.Entities;
using MapDat.Application.Models.GeoObjects;

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
                Coordinates = entity.Geometry.Coordinates[0].ToString()
            };
        }
        public string Id { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public PropertiesViewModel Properties { get; set; } = null!;
        public GeometryViewModel Geometry { get; set; } = null!;
    }
}
