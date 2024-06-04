using MapDat.Application.Features.Common;
using MapDat.Application.Models.RSPO;


namespace MapDat.Application.Features.RSPO.Queries
{
   public class GetInfoQuery : BaseRequest<IEnumerable<InfoViewModel>>
    {
        public IEnumerable<DataObject> Data = new List<DataObject>();
    }
}
