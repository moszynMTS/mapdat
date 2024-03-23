using MapDat.Application.Features.Common;

namespace MapDat.Application.Features.Cargoes.Commands.UpdateCargo
{
    public class UpdateCargoCommand : BaseIdentifiableRequest
    {
        public string Test { get; set; } = null!;
    }
}
