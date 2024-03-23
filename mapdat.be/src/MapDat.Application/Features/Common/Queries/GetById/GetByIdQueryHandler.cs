using AutoMapper;
using MapDat.Domain.Common;
using MapDat.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MapDat.Application.Features.Common.Queries.GetById
{
    public class GetByIdQueryHandler<TEntity, TRequest, TResponse> : BaseQueryHandler<TEntity, TRequest, TResponse>
        where TEntity : BaseAuditableEntity
        where TRequest : GetByIdQuery<TResponse>
        where TResponse : class
    {
        public GetByIdQueryHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        public override async Task<BaseResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = GetFilteredQuery(request);
            TEntity? entity = await query.FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);

            if (entity != null)
            {
                TResponse content = Mapper.Map<TResponse>(entity);
                return new BaseResponse<TResponse>(statusCode: HttpStatusCode.OK, content: content);
            }
            else
            {
                return new BaseResponse<TResponse>(statusCode: HttpStatusCode.NotFound, content: null,
                    error: $"Object of type \"{typeof(TEntity).Name}\" with Id = {request.Id} was not found.");
            }
        }

        protected virtual IQueryable<TEntity> GetFilteredQuery(TRequest request)
        {
            return DbSet.Where(x => x.IsActive.Equals(true));
        }
    }
}
