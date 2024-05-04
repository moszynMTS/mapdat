using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Queries;
using MapDat.Application.Models.Wojewodztwa;
using MapDat.Persistance.Services;
using System.Net;

namespace MapDat.Application.Features.Powiaty.Queries
{
    public class GetPowiatyQueryHandler : BaseQueryHandler<GetPowiatyQuery, IEnumerable<PowiatyViewModel>>
    {
        public GetPowiatyQueryHandler(IMongoService mongoService) 
            : base(mongoService) { }
        public async override Task<BaseResponse<IEnumerable<PowiatyViewModel>>> Handle(GetPowiatyQuery request, CancellationToken cancellationToken)
        {
            var entities = await _mongoService.GetPowiaty(request.Wojewodztwo);
            var result = new List<PowiatyViewModel>();
            foreach (var entity in entities)
                result.Add(new PowiatyViewModel(entity));
            return new BaseResponse<IEnumerable<PowiatyViewModel>>(statusCode: HttpStatusCode.OK, content: result);
        }
    }
}
