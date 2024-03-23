using MapDat.Application.Features.Common;
using MapDat.Application.Models.Cargoes;

namespace MapDat.Application.Features.Cargoes.Commands.CreateCargo
{
    public class CreateCargoCommand : BaseRequest<CargoViewModel>
    {
        public string Test { get; set; } = null!;
    }
}
