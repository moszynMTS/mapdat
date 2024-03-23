using AutoMapper;
using MapDat.Application.Features.Common.Commands.Update;
using MapDat.Domain.Entities;
using MapDat.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace MapDat.Application.Features.Cargoes.Commands.UpdateCargo
{
    public class UpdateCargoCommandHandler : UpdateCommandHandler<CargoEntity, UpdateCargoCommand>
    {
        public UpdateCargoCommandHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper) { }

        public override async Task<CargoEntity?> getEntityAsync(UpdateCargoCommand request, CancellationToken cancellationToken)
        {
            return await DbSet
                            .FirstOrDefaultAsync(x => x.Id.Equals(request.Id), cancellationToken);
        }

        
    }
}
