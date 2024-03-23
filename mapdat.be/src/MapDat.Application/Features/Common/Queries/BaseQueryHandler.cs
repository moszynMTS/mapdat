using AutoMapper;
using MapDat.Domain.Common;
using MapDat.Persistance.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MapDat.Application.Features.Common.Queries
{
    public abstract class BaseQueryHandler<TEntity, TRequest, TResponse> : IRequestHandler<TRequest, BaseResponse<TResponse>>
        where TEntity : BaseAuditableEntity
        where TRequest : IRequest<BaseResponse<TResponse>>
        where TResponse : class
    {
        protected IMapDatDbContext DbContext;
        protected IMapper Mapper;
        protected DbSet<TEntity> DbSet { get; set; }

        protected BaseQueryHandler(IMapDatDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
            DbSet = DbContext.Set<TEntity>();
        }

        public abstract Task<BaseResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
