namespace MapDat.Application.Features.Common
{
    public class SelectlistResult
    {
        public SelectlistResult() { }
        public SelectlistResult(string id, string label)
        {
            Id = id;
            Label = label;
        }

        public string Id { get; set; }
        public string Label { get; set; } = null!;
    }
}
