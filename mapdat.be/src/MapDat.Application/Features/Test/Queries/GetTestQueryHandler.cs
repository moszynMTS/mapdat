using AutoMapper;
using MapDat.Application.Features.Cargoes.Queries.GetCargoById;
using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Queries;
using MapDat.Application.Features.Common.Queries.GetById;
using MapDat.Application.Models.Cargoes;
using MapDat.Application.Models.Test;
using MapDat.Domain.Entities;
using MapDat.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MapDat.Application.Features.Test.Queries
{
    public class GetTestQueryHandler : BaseQueryHandler<CargoEntity, GetTestQuery, TestViewModel>
    {
        public GetTestQueryHandler(IMapDatDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override Task<BaseResponse<TestViewModel>> Handle(GetTestQuery request, CancellationToken cancellationToken)
        {
            var result = new TestViewModel() { Name = DateTime.Now.ToString() };
            return Task.FromResult(
                new BaseResponse<TestViewModel>(statusCode: HttpStatusCode.OK, content: result)
                );
        }
    }
}
