using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Queries;
using MapDat.Application.Models.RSPO;
using MapDat.Persistance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDat.Application.Features.RSPO.Queries
{
    public class GetInfoQueryHandler : BaseQueryHandler<GetInfoQuery, IEnumerable<InfoViewModel>>
    {
        public GetInfoQueryHandler(IMongoService mongoService)
            : base(mongoService) { }

        public override Task<BaseResponse<IEnumerable<InfoViewModel>>> Handle(GetInfoQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
