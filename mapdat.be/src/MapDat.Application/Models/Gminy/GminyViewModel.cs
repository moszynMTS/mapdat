using MapDat.Domain.Entities;
using MapDat.Application.Models.GeoObjects;

namespace MapDat.Application.Models.Wojewodztwa
{
    public class GminyViewModel : MyGeoObjectViewModel<GminyPropertiesViewModel>
    {
        public GminyViewModel(GminaEntity entity)
        {
            Id = entity.Id;
            Type = entity.Type;
            Properties = new GminyPropertiesViewModel()
            {
                Name = entity.Properties.Name,
                Powiat = entity.Properties.Powiat
            };
            Geometry = new GeometryViewModel()
            {
                Type = entity.Geometry.Type,
                Coordinates = ParseBisonArray(entity.Geometry.Coordinates)
            };
        }
        
    }
}