namespace MapDat.Application.Features.Common.Queries.GetPageable
{
    public class GetPageableQuery<TResponse> : BaseRequest<PaginationResult<TResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; }
        public bool Desc { get; set; }
        public string? OrderBy { get; set; }
    }
}