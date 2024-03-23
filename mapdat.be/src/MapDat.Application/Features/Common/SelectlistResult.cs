namespace MapDat.Application.Features.Common
{
    public class SelectlistResult
    {
        public SelectlistResult() { }
        public SelectlistResult(Guid id, string label)
        {
            Id = id;
            Label = label;
        }

        public Guid Id { get; set; }
        public Guid? ProjectId { get; set; }
        public string Label { get; set; } = null!;
    }
}
