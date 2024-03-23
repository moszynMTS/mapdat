namespace MapDat.Application.Features.Common.Queries.GetSelectable
{
    public class GetSelectableQuery : BaseRequest<IEnumerable<SelectlistResult>>
    {
        public string? FilterWords { get; set; }
    }
}
