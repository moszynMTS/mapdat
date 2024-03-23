using AutoMapper;
using MapDat.Domain.Common;
using MapDat.Domain.Entities;
using MapDat.Persistance.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MapDat.Application.Features.Common.Commands
{
    public abstract class BaseCommandHandler<TEntity, TRequest> : IRequestHandler<TRequest, BaseResponse>
        where TEntity : BaseAuditableEntity
        where TRequest : IRequest<BaseResponse>
    {
        protected readonly IMapDatDbContext DbContext;
        protected readonly IMapper Mapper;
        protected DbSet<TEntity> DbSet { get; set; }

        protected BaseCommandHandler(IMapDatDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
            DbSet = DbContext.Set<TEntity>();
        }

        public abstract Task<BaseResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class BaseCommandHandler<TEntity, TRequest, TResponse> : IRequestHandler<TRequest, BaseResponse<TResponse>>
        where TEntity : BaseAuditableEntity
        where TRequest : IRequest<BaseResponse<TResponse>>
    {
        protected readonly IMapDatDbContext DbContext;
        protected readonly IMapper Mapper;
        protected DbSet<TEntity> DbSet { get; set; }
        protected BaseCommandHandler(IMapDatDbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
            DbSet = DbContext.Set<TEntity>();
        }

        public abstract Task<BaseResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}

