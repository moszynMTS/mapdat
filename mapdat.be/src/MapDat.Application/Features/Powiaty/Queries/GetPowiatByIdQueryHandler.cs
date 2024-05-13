using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Queries;
using MapDat.Application.Models.Wojewodztwa;
using MapDat.Persistance.Services;
using System.Net;

namespace MapDat.Application.Features.Powiaty.Queries
{
    public class GetPowiatByIdQueryHandler : BaseQueryHandler<GetPowiatByIdQuery, PowiatyViewModel>
    {
        public GetPowiatByIdQueryHandler(IMongoService mongoService) 
            : base(mongoService) { }
        public async override Task<BaseResponse<PowiatyViewModel>> Handle(GetPowiatByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = _mongoService.GetPowiat(request.Id);
            var result = new PowiatyViewModel(entity);
            return new BaseResponse<PowiatyViewModel>(statusCode: HttpStatusCode.OK, content: result);
        }
    }
}
