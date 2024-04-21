namespace MapDat.Application.Features.Common
{
    public class BaseIdentifiableRequest : BaseRequest
    {
        public Guid Id { get; set; }
    }

    public class BaseIdentifiableRequest<TResponse> : BaseRequest<TResponse>
    {
        public Guid Id { get; set; }
    }
}
