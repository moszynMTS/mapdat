using MapDat.Application.Features.Common;
using MapDat.Application.Models.Wojewodztwa;
using MapDat.Domain.Entities;

namespace MapDat.Application.Features.Wojewodztwa.Queries
{
    public class GetWojewodztwaQuery : BaseRequest<IEnumerable<WojewodztwaViewModel>>
    {
    }
}
