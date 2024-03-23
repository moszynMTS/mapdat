using AutoMapper;
using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Queries.GetById;
using MapDat.Application.Models.Cargoes;

using MapDat.Domain.Entities;
using MapDat.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MapDat.Application.Features.Cargoes.Queries.GetCargoById
{
    public class GetCargoByIdQueryHandler : GetByIdQueryHandler<CargoEntity, GetCargoByIdQuery, CargoViewModel>
    {
        public GetCargoByIdQueryHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    }
}
