using MapDat.Application.Features.Common;
using MapDat.Application.Models.Wojewodztwa;

namespace MapDat.Application.Features.Powiaty.Queries
{
    public class GetPowiatyQuery : BaseRequest<IEnumerable<PowiatyViewModel>>
    {
        public string Wojewodztwo { get; set; } = null!;
    }
}
