using MapDat.Application.Features.Common;
using MapDat.Application.Features.Test.Queries;
using MapDat.Application.Features.Wojewodztwa.Queries;
using MapDat.Application.Models.Test;
using MapDat.Application.Models.Wojewodztwa;
using Microsoft.AspNetCore.Mvc;

namespace MapDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WojewodztwaController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<WojewodztwaViewModel>>>> GetWojewodztwa([FromQuery] GetWojewodztwaQuery request) =>
            await Mediator.Send(request);
    }
}
