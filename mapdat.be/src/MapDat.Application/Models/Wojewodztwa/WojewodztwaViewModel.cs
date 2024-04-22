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

        private List<List<List<List<double>>>> ParseBisonArray(BsonArray[] coordinates)
        {
            List<List<List<List<double>>>> result = new List<List<List<List<double>>>>();
            for (int i = 0; i < coordinates.Length; i++)
            {
                var x = coordinates[i];
                List<List<List<double>>> test = new List<List<List<double>>>();
                foreach (var item in x.AsBsonArray)
                {
                    var s = item.ToString();
                    s = s.Replace(" ", "").Replace("[", "").Replace("]", "");
                    var tmp = s.Split(',');
                    List<List<double>> doubles = new List<List<double>>();
                    for (int j = 0; j < tmp.Length/2; j++)
                    {
                        List<double> values = new List<double>();
                        for (int k = 0; k < 2; k++)
                        {
                            string xaa = tmp[j+k];
                            string res = xaa.Substring(xaa.IndexOf('.') + 1, Math.Min(4, xaa.Length - xaa.IndexOf('.') - 1));
                            var y = xaa.Split('.');
                            xaa = y[0] + ',' + res;
                            values.Add(double.Parse(xaa));
                        }
                        doubles.Add(values);
                        j++;
                    }
                    test.Add(doubles);
                }
                result.Add(test);
            }
            return result;
        }
    }
}