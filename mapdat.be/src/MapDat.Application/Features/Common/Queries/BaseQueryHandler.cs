using AutoMapper;
using MapDat.Domain.Common;
using MapDat.Persistance.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MapDat.Persistance.Services;

namespace MapDat.Application.Features.Common.Queries
{
    public abstract class BaseQueryHandler<TEntity, TRequest, TResponse> : IRequestHandler<TRequest, BaseResponse<TResponse>>
        where TEntity : BaseAuditableEntity
        where TRequest : IRequest<BaseResponse<TResponse>>
        where TResponse : class
    {
        protected IMapDatDbContext DbContext;
        protected readonly IWojewodztwaService _wojewodztwaService;

        protected IMapper Mapper;
        protected DbSet<TEntity> DbSet { get; set; }

        protected BaseQueryHandler(IMapDatDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
            DbSet = DbContext.Set<TEntity>();
        }
        protected BaseQueryHandler(IWojewodztwaService wojewodztwaService, IMapper mapper)
        {
            Mapper = mapper;
            _wojewodztwaService = wojewodztwaService;
        }
        public abstract Task<BaseResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
