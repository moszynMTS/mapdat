using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Queries;
using MapDat.Application.Models.Wojewodztwa;
using MapDat.Persistance.Services;
using System.Net;

namespace MapDat.Application.Features.Gminy.Queries
{
    public class GetGminyQueryHandler : BaseQueryHandler<GetGminyQuery, IEnumerable<GminyViewModel>>
    {
        public GetGminyQueryHandler(IMongoService mongoService) 
            : base(mongoService) { }
        public async override Task<BaseResponse<IEnumerable<GminyViewModel>>> Handle(GetGminyQuery request, CancellationToken cancellationToken)
        {
            var entities = await _mongoService.GetGminy(request.Powiat);
            var result = new List<GminyViewModel>();
            foreach (var entity in entities)
                result.Add(new GminyViewModel(entity));
            return new BaseResponse<IEnumerable<GminyViewModel>>(statusCode: HttpStatusCode.OK, content: result);
        }
    }
}
