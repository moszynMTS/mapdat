using MapDat.Domain.Entities;
using MapDat.Application.Models.GeoObjects;

namespace MapDat.Application.Models.Wojewodztwa
{
    public class PowiatyViewModel : MyGeoObjectViewModel<PowiatyPropertiesViewModel>
    {
        public PowiatyViewModel() { }
        public PowiatyViewModel(PowiatEntity entity)
        {
            Id = entity.Id;
            Type = entity.Type;
            Properties = new PowiatyPropertiesViewModel()
            {
                Name = entity.Properties.Name,
                Wojewodztwo = entity.Properties.Wojewodztwo
            };
            Geometry = new GeometryViewModel()
            {
                Type = entity.Geometry.Type,
                Coordinates = ParseBisonArray(entity.Geometry.Coordinates)
            };
        }
        
    }
}