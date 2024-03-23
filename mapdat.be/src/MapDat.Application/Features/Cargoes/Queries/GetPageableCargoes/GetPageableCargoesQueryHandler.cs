using AutoMapper;
using MapDat.Application.Features.Common.Queries.GetPageable;
using MapDat.Application.Models.Cargoes;
using MapDat.Domain.Entities;
using MapDat.Domain.Enums;
using MapDat.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MapDat.Application.Features.Cargoes.Queries.GetPageableCargoes
{
    public class GetPageableCargoesQueryHandler : GetPageableQueryHandler<CargoEntity, GetPageableCargoesQuery, CargoViewModel>
    {
        public GetPageableCargoesQueryHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        protected override Expression<Func<CargoEntity, bool>> FilterExpression(string word)
        {
            return c =>
                (EF.Functions.Like(c.Test, word));
        }
        protected override IQueryable<CargoEntity> GetFilteredQuery(GetPageableCargoesQuery request)
        {
            var query = base.GetFilteredQuery(request);
            return query;
        }
        protected override IQueryable<CargoEntity> GetOrderBy(IQueryable<CargoEntity> query, string? orderBy, bool desc)
        {
            orderBy = orderBy?.ToLower();
            return (orderBy, desc) switch
            {
                ("test", true) => query.OrderByDescending(_ => _.Test),

                ("test", false) => query.OrderBy(_ => _.Test),

                _ => query.OrderByDescending(_ => _.Created)
            };
        }
    }
}
