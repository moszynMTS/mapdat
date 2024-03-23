using AutoMapper;
using MapDat.Domain.Common;
using MapDat.Domain.Entities;
using MapDat.Domain.Enums;
using MapDat.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;

namespace MapDat.Application.Features.Common.Commands.Update
{
    public class UpdateCommandHandler<TEntity, TRequest> : BaseCommandHandler<TEntity, TRequest>
    where TEntity : BaseAuditableEntity
    where TRequest : BaseIdentifiableRequest
    {
        public UpdateCommandHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        public override async Task<BaseResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            TEntity? entity = getEntityAsync(request, cancellationToken).Result;
            if (entity != null)
            {
                Mapper.Map(request, entity);
                await DbContext.SaveChangesAsync(cancellationToken);
                return new BaseResponse(statusCode: HttpStatusCode.OK);
            }
            else
            {
                return new BaseResponse(statusCode: HttpStatusCode.NotFound,
                    error: $"Object of type \"{typeof(TEntity).Name}\" with Id = {request.Id} was not found.");
            }
        }

        public virtual async Task<TEntity?> getEntityAsync(TRequest request, CancellationToken cancellationToken)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
        }
    }
}
