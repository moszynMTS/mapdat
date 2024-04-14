using MapDat.Application.Features.Common;
using Microsoft.AspNetCore.Mvc;
using MapDat.Application.Features.Test.Queries;
using MapDat.Application.Models.Test;

namespace MapDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<BaseResponse<TestViewModel>>> GetTest([FromQuery] GetTestQuery request) =>
            await Mediator.Send(request);

    }
}
