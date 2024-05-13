using MapDat.Application.Features.Common;
using MapDat.Application.Models.Wojewodztwa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDat.Application.Features.Powiaty.Queries
{
    public class GetPowiatByIdQuery : BaseRequest<PowiatyViewModel>
    {
        public string Id { get; set; } = null!;
    }
}
