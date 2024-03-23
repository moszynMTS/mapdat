namespace MapDat.Application.Features.Common
{
    public class PaginationResult<TViewModel>
    {
        public PaginationResult(IEnumerable<TViewModel> result, int total)
        {
            Result = result;
            Total = total;
        }

        public IEnumerable<TViewModel> Result { get; }

        public int Total { get; set; }
    }
}
