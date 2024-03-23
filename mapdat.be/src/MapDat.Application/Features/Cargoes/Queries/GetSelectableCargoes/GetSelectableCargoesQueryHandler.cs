using AutoMapper;
using MapDat.Application.Features.Common.Queries.GetSelectable;
using MapDat.Domain.Entities;
using MapDat.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MapDat.Application.Features.Cargoes.Queries.GetSelectableCargoes
{
    public class GetSelectableCargoesQueryHandler : GetSelectableQueryHandler<CargoEntity, GetSelectableCargoesQuery>
    {
        public GetSelectableCargoesQueryHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        protected override Expression<Func<CargoEntity, bool>> FilterExpression(string word)
        {
            return c =>
                EF.Functions.Like(c.Test, word);
        }
    }
}
