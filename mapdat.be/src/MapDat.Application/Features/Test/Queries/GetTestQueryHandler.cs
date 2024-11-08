﻿using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Queries;
using MapDat.Application.Models.Test;
using MapDat.Persistance.Services;
using System.Net;

namespace MapDat.Application.Features.Test.Queries
{
    public class GetTestQueryHandler : BaseQueryHandler<GetTestQuery, TestViewModel>
    {
        public GetTestQueryHandler(IMongoService mongoService) 
            : base(mongoService) { }
        public async override Task<BaseResponse<TestViewModel>> Handle(GetTestQuery request, CancellationToken cancellationToken)
        {
            var result = new TestViewModel() { Name = DateTime.Now.ToString() };
            return new BaseResponse<TestViewModel>(statusCode: HttpStatusCode.OK, content: result);
        }
    }
}
