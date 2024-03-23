using AutoMapper;
using MapDat.Domain.Common;
using MapDat.Persistance.Context;
using MapDat.Persistance.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace MapDat.Application.Features.Common.Queries.GetSelectable
{
    public abstract class GetSelectableQueryHandler<TEntity, TRequest> : BaseQueryHandler<TEntity, TRequest, IEnumerable<SelectlistResult>>
        where TEntity : BaseAuditableEntity
        where TRequest : GetSelectableQuery
    {
        public GetSelectableQueryHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        public override async Task<BaseResponse<IEnumerable<SelectlistResult>>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            IQueryable<TEntity> query = GetFilteredQuery(request);

            List<TEntity> entities = await query
                .ForSearchTerm(request.FilterWords, FilterExpression)
                .OrderByDescending(x => x.Created)
                .Take(50)
                .ToListAsync();

            IEnumerable<SelectlistResult> result = Mapper.Map<IEnumerable<SelectlistResult>>(entities);
            entities.Clear();
            return new BaseResponse<IEnumerable<SelectlistResult>>(HttpStatusCode.OK, result);
        }

        protected virtual IQueryable<TEntity> GetFilteredQuery(TRequest request)
        {
            return DbSet.Where(x => x.IsActive.Equals(true));
        }

        protected abstract Expression<Func<TEntity, bool>> FilterExpression(string word);
    }
}
