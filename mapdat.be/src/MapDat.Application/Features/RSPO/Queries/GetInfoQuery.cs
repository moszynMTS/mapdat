using MapDat.Application.Features.Common;
using MapDat.Application.Models.RSPO;


namespace MapDat.Application.Features.RSPO.Queries
{
   public class GetInfoQuery : BaseRequest<IEnumerable<InfoViewModel>>
    {
        public IEnumerable<string> Wojewodztwa { get; set; } = new List<string>();
        public IEnumerable<string> Powiaty { get; set; } = new List<string>();
        public IEnumerable<string> Gminy { get; set; } = new List<string>();
        public IEnumerable<string> Subjects { get; set; } = new List<string>();
    }
}
