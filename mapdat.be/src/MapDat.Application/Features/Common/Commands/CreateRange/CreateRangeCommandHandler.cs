using AutoMapper;
using MapDat.Domain.Common;
using MapDat.Domain.Entities;
using MapDat.Domain.Enums;
using MapDat.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MapDat.Application.Features.Common.Commands.CreateRange
{
    public class CreateRangeCommandHandler<TEntity, TRequest, TCommand, TResponse> : BaseCommandHandler<TEntity, TRequest, IEnumerable<TResponse>>
        where TEntity : BaseAuditableEntity
        where TRequest : CreateRangeCommand<TCommand, TResponse>
    {
        public int _MaxLpForHistoryGroup = 1;
        public int _MaxLpForHistory = 1;
        public bool _AddHistory = false;
        public CreateRangeCommandHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        public override async Task<BaseResponse<IEnumerable<TResponse>>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(request.Commands);

            if (entities.Any())
            {
                var entitiesInBase = await EntitiesInBase(request, cancellationToken);
                var entitiesToAdd = await EntitiesToAdd(entitiesInBase, entities);
                var entitiesToUpdate = await EntitiesToUpdateAsync(entitiesInBase, entities);
                var entitiesToDelete = await EntitiesToDeleteAsync(entitiesInBase, entities);

                var status = await CheckStatusAsync(entitiesToUpdate, entities, request);

                await DbSet.AddRangeAsync(entitiesToAdd, cancellationToken);

                await DbContext.SaveChangesAsync(cancellationToken);
                var models = Mapper.Map<IEnumerable<TResponse>>(entities);
                return new BaseResponse<IEnumerable<TResponse>>(statusCode: HttpStatusCode.Created, models);
            }
            else
            {
                return new BaseResponse<IEnumerable<TResponse>>(statusCode: HttpStatusCode.NotFound, content: null,
                    error: $"List of type \"{typeof(TEntity).Name}\" has no elements.");
            }
        }

        public async Task<IEnumerable<TEntity>> EntitiesToAdd(IEnumerable<TEntity> entitiesInBase, IEnumerable<TEntity> expextedEntities)
        {
            List<TEntity> result = new List<TEntity>();
            foreach (TEntity entity in expextedEntities)
            {
                if (!entitiesInBase.Any(x => x.Id == entity.Id))
                {
                    if (entity.Id == Guid.Empty)
                    {
                        entity.Id = Guid.NewGuid();
                    }
                    result.Add(entity);
                }
            }
            return result;
        }
        public virtual async Task<IEnumerable<TEntity>> EntitiesToUpdateAsync(IEnumerable<TEntity> entitiesInBase, IEnumerable<TEntity> expextedEntities)
        {
            return new List<TEntity>();
        }
        public virtual async Task<IEnumerable<TEntity>> EntitiesToDeleteAsync(IEnumerable<TEntity> entitiesInBase, IEnumerable<TEntity> expextedEntities)
        {
            List<TEntity> result = new List<TEntity>();
            foreach (TEntity entity in entitiesInBase)
            {
                if (!expextedEntities.Any(x => x.Id == entity.Id))
                { 
                    entity.IsActive = false;
                    result.Add(entity);
                }
            }
            return result;
        }
        public virtual async Task<IEnumerable<TEntity>> EntitiesInBase(TRequest request, CancellationToken cancellationToken)
        {
            return new List<TEntity>();
        }
        public virtual async Task<int> CheckStatusAsync(IEnumerable<TEntity> entitiesToUpdate, IEnumerable<TEntity> expextedEntities,TRequest request)
        {
            return 0;
        }
    }
}
