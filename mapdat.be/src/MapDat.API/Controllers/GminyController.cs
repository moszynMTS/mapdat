using MapDat.Application.Features.Common;
using MapDat.Application.Features.Gminy.Queries;
using MapDat.Application.Features.Powiaty.Queries;
using MapDat.Application.Features.Wojewodztwa.Queries;
using MapDat.Application.Models.Wojewodztwa;
using Microsoft.AspNetCore.Mvc;

namespace MapDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GminyController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<BaseResponse<IEnumerable<GminyViewModel>>>> GetGminy([FromQuery] GetGminyQuery request) =>
            await Mediator.Send(request);

        /*[HttpGet("ById")]
        public async Task<ActionResult<BaseResponse<PowiatyViewModel>>> GetGmina([FromQuery] GetGminaByIdQuery request) =>
                await Mediator.Send(request);*/
    }
}