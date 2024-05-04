using MapDat.Domain.Common;
using MapDat.Persistance.Services;
using MediatR;

namespace MapDat.Application.Features.Common.Commands
{
    public abstract class BaseCommandHandler<TEntity, TRequest> : IRequestHandler<TRequest, BaseResponse>
        where TEntity : BaseEntity
        where TRequest : IRequest<BaseResponse>
    {
        protected readonly IMongoService _mongoService;
        protected BaseCommandHandler(IMongoService mongoService)
        {
            _mongoService = mongoService;
        }
        public abstract Task<BaseResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class BaseCommandHandler<TEntity, TRequest, TResponse> : IRequestHandler<TRequest, BaseResponse<TResponse>>
        where TEntity : BaseEntity
        where TRequest : IRequest<BaseResponse<TResponse>>
    {
        protected readonly IMongoService _mongoService;
        protected BaseCommandHandler(IMongoService mongoService)
        {
            _mongoService = mongoService;
        }
        public abstract Task<BaseResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}

