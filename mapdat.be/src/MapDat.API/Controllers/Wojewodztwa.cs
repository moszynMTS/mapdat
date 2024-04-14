using MapDat.Application.Features.Common;
using MapDat.Application.Features.Test.Queries;
using MapDat.Application.Models.Test;
using Microsoft.AspNetCore.Mvc;

namespace MapDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WojewodztwaController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<BaseResponse<TestViewModel>>> GetWojewodztwa([FromQuery] GetTestQuery request) =>
            await Mediator.Send(request);
    }
}
