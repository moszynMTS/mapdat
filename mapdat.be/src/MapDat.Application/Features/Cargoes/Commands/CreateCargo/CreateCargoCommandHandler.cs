using AutoMapper;
using MapDat.Application.Features.Common.Commands.Create;
using MapDat.Application.Models.Cargoes;

using MapDat.Domain.Entities;
using MapDat.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace MapDat.Application.Features.Cargoes.Commands.CreateCargo
{
    public class CreateCargoCommandHandler : CreateCommandHandler<CargoEntity, CreateCargoCommand, CargoViewModel>
    {
        public CreateCargoCommandHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        public override async Task<CargoEntity?> getEntityAsync(Guid newEntityId, CancellationToken cancellationToken)
        {
            return await DbSet
                              .FirstOrDefaultAsync(x => x.Id.Equals(newEntityId), cancellationToken);
        }
    }
}
