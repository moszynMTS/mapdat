using AutoMapper;
using MapDat.Domain.Common;
using MapDat.Persistance.Context;
using MapDat.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace MapDat.Application.Features.Common.Queries.GetPageable
{
    public abstract class GetPageableQueryHandler<TEntity, TRequest, TResponse> : BaseQueryHandler<TEntity, TRequest, PaginationResult<TResponse>>
        where TEntity : BaseAuditableEntity
        where TRequest : GetPageableQuery<TResponse>
    {
        public GetPageableQueryHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        public override async Task<BaseResponse<PaginationResult<TResponse>>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var result = await GetResultAsync(request);
            return new BaseResponse<PaginationResult<TResponse>>(HttpStatusCode.OK, result);
        }

        protected virtual async Task<PaginationResult<TResponse>> GetResultAsync(TRequest request)
        {
            var queryTotal = GetSearchTermFilteredQuery(request);
            var query = GetOrderBy(queryTotal, request.OrderBy, request.Desc);
            if (request.PageNumber > 1)
            {
                query = query.Skip((request.PageNumber - 1) * request.PageSize);
            }
            query = query.Take(request.PageSize);

            var entities = await query.ToListAsync();

            var totalTask = queryTotal.CountAsync();
            var result = Mapper.Map<IEnumerable<TResponse>>(entities);
            entities.Clear();
            return new PaginationResult<TResponse>(result, await totalTask);
        }

        protected IQueryable<TEntity> GetSearchTermFilteredQuery(TRequest request)
        {
            var query = GetFilteredQuery(request);
            return query.ForSearchTerm(request.SearchTerm, FilterExpression);
        }

        protected virtual IQueryable<TEntity> GetFilteredQuery(TRequest request)
        {
            return DbSet.Where(x => x.IsActive.Equals(true));
        }

        protected abstract Expression<Func<TEntity, bool>> FilterExpression(string word);
        protected abstract IQueryable<TEntity> GetOrderBy(IQueryable<TEntity> query, string? orderBy, bool desc);
    }
}
