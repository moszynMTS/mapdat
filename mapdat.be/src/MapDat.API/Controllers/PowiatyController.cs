using MapDat.Application.Features.Common;
using MapDat.Application.Features.Powiaty.Queries;
using MapDat.Application.Features.Wojewodztwa.Queries;
using MapDat.Application.Models.Wojewodztwa;
using Microsoft.AspNetCore.Mvc;

namespace MapDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PowiatyController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<PowiatyViewModel>>>> GetPowiaty([FromQuery] GetPowiatyQuery request) =>
            await Mediator.Send(request);
    }
}
