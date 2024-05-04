using MapDat.Domain.Entities;
using MapDat.Application.Models.GeoObjects;

namespace MapDat.Application.Models.Wojewodztwa
{
    public class WojewodztwaViewModel : MyGeoObjectViewModel<WojewodztwaPropertiesViewModel>
    {
        public WojewodztwaViewModel(WojewodztwoEntity entity)
        {
            Id = entity.Id;
            Type = entity.Type;
            Properties = new WojewodztwaPropertiesViewModel()
            {
                Name = entity.Properties.Name
            };
            Geometry = new GeometryViewModel()
            {
                Type = entity.Geometry.Type,
                Coordinates = ParseBisonArray(entity.Geometry.Coordinates)
            };
        }
        
    }
}