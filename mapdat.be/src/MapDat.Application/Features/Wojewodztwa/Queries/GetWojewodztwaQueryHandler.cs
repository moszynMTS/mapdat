using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Queries;
using MapDat.Application.Models.Wojewodztwa;
using MapDat.Persistance.Services;
using System.Net;

namespace MapDat.Application.Features.Wojewodztwa.Queries
{
    public class GetWojewodztwaQueryHandler : BaseQueryHandler<GetWojewodztwaQuery, IEnumerable<WojewodztwaViewModel>>
    {
        public GetWojewodztwaQueryHandler(IWojewodztwaService wojewodztwaService) 
            : base(wojewodztwaService) { }
        public async override Task<BaseResponse<IEnumerable<WojewodztwaViewModel>>> Handle(GetWojewodztwaQuery request, CancellationToken cancellationToken)
        {
            var wojewodztwa = await _wojewodztwaService.GetWojewodztwa();
            var result = new List<WojewodztwaViewModel>();

            foreach(var wojewodztwo in wojewodztwa)
                result.Add(new WojewodztwaViewModel { GeoJson=ToGeoJson(wojewodztwo) });

            return new BaseResponse<IEnumerable<WojewodztwaViewModel>>(statusCode: HttpStatusCode.OK, content: result);
        }
    }
}
