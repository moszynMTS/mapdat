using MapDat.Application.Features.Common;
using MapDat.Application.Models.Wojewodztwa;

namespace MapDat.Application.Features.Gminy.Queries
{
    public class GetGminyQuery : BaseRequest<IEnumerable<GminyViewModel>>
    {
        public string Powiat { get; set; } = null!;
        public string PowiatId { get; set; } = null!;
    }
}
