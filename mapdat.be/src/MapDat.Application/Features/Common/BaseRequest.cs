using MediatR;

namespace MapDat.Application.Features.Common
{
    public class BaseRequest : IRequest<BaseResponse>
    {
    }

    public class BaseRequest<TResponse> : IRequest<BaseResponse<TResponse>>
    {
    }
}
