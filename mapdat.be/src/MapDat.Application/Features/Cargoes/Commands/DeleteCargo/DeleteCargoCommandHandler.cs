using AutoMapper;
using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Commands.Delete;
using MapDat.Domain.Entities;
using MapDat.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace MapDat.Application.Features.Cargoes.Commands.DeleteCargo
{
    public class DeleteCargoCommandHandler : DeleteCommandHandler<CargoEntity, DeleteCargoCommand>
    {
        public DeleteCargoCommandHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }
    }
}
