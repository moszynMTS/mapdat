using AutoMapper;
using MapDat.Application.Features.Cargoes.Queries.GetCargoById;
using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Queries;
using MapDat.Application.Features.Common.Queries.GetById;
using MapDat.Application.Models.Cargoes;
using MapDat.Application.Models.Test;
using MapDat.Domain.Entities;
using MapDat.Persistance.Context;
using MapDat.Persistance.Services;
using System.Net;

namespace MapDat.Application.Features.Test.Queries
{
    public class GetTestQueryHandler : BaseQueryHandler<CargoEntity, GetTestQuery, TestViewModel>
    {
        public GetTestQueryHandler(IWojewodztwaService wojewodztwaService, IMapper mapper) 
            : base(wojewodztwaService, mapper) { }

        public async override Task<BaseResponse<TestViewModel>> Handle(GetTestQuery request, CancellationToken cancellationToken)
        {
            var test = await _wojewodztwaService.GetWojewodztwa();
            var result = new TestViewModel() { Name = DateTime.Now.ToString() };
            return new BaseResponse<TestViewModel>(statusCode: HttpStatusCode.OK, content: result);
                
        }
    }
}
