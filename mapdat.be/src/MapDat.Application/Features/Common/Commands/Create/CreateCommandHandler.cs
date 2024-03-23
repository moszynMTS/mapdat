using AutoMapper;
using MapDat.Domain.Common;
using MapDat.Domain.Entities;
using MapDat.Domain.Enums;
using MapDat.Persistance.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MapDat.Application.Features.Common.Commands.Create
{
    public class CreateCommandHandler<TEntity, TRequest> : BaseCommandHandler<TEntity, TRequest>
        where TEntity : BaseAuditableEntity
        where TRequest : IRequest<BaseResponse>
    {
        public CreateCommandHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        public override async Task<BaseResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<TEntity>(request);
            await DbSet.AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            
            return new BaseResponse(statusCode: HttpStatusCode.Created);
        }

        public virtual async Task<TEntity?> getEntityAsync(Guid newEntityId, CancellationToken cancellationToken)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(newEntityId), cancellationToken);
        }
    }

    public class CreateCommandHandler<TEntity, TRequest, TResponse> : BaseCommandHandler<TEntity, TRequest, TResponse>
    where TEntity : BaseAuditableEntity
    where TRequest : IRequest<BaseResponse<TResponse>>
    {
        public CreateCommandHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        public override async Task<BaseResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<TEntity>(request);
            await DbSet.AddAsync(entity, cancellationToken);
            await DbContext.SaveChangesAsync(cancellationToken);
            var model = Mapper.Map<TResponse>(entity);

            return new BaseResponse<TResponse>(statusCode: HttpStatusCode.Created, content: model);
        }

        public virtual async Task<TEntity?> getEntityAsync(Guid newEntityId, CancellationToken cancellationToken)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(newEntityId), cancellationToken);
        }
    }
}
