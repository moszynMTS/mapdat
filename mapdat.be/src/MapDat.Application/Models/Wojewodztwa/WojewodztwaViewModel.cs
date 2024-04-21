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

        private List<double[]> ParseBisonArray(BsonArray coordinates)
        {
            BsonValue x = coordinates[0];
            List<double[]> test = new List<double[]>();

            foreach (var item in x.AsBsonArray)
            {
                var s = item.ToString();
                s = s.Replace(" ", "").Replace("[", "").Replace("]", "");
                var tmp = s.Split(',');
                double[] doubles = new double[tmp.Length];
                for(int i=0; i<tmp.Length; i++)
                {
                    string xaa = tmp[i];
                    string res = xaa.Substring(xaa.IndexOf('.') + 1, Math.Min(4, xaa.Length - xaa.IndexOf('.') - 1));
                    var y = xaa.Split('.');
                    tmp[i] = y[0]+','+res;
                    doubles[i] = double.Parse(tmp[i]);
                }
                test.Add(doubles);
            }
            return test;
        }
    }
}