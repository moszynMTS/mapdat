namespace MapDat.Application.Models.GeoObjects
{
    public class GeometryViewModel
    {
        public string Type { get; set; } = String.Empty;
        public List<List<List<List<double>>>> Coordinates { get; set; } = new List<List<List<List<double>>>>();
    }
}
