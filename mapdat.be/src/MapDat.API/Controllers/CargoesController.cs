using MapDat.Application.Features.Common;
using MapDat.Application.Features.Cargoes.Commands.CreateCargo;
using MapDat.Application.Features.Cargoes.Commands.UpdateCargo;
using MapDat.Application.Features.Cargoes.Queries.GetCargoById;
using MapDat.Application.Features.Cargoes.Queries.GetPageableCargoes;
using MapDat.Application.Features.Cargoes.Queries.GetSelectableCargoes;
using MapDat.Application.Models.Cargoes;
using Microsoft.AspNetCore.Mvc;
using MapDat.Application.Features.Cargoes.Commands.DeleteCargo;


namespace MapDat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoesController : BaseApiController
    {
        [HttpGet("{Id}")]
        public async Task<ActionResult<BaseResponse<CargoViewModel>>> GetCargoById([FromRoute] GetCargoByIdQuery request) =>
            await Mediator.Send(request);

        [HttpGet("pageable")]
        public async Task<ActionResult<BaseResponse<PaginationResult<CargoViewModel>>>> GetPageableCargoes([FromQuery] GetPageableCargoesQuery request) =>
            await Mediator.Send(request);

        [HttpGet("selectable")]
        public async Task<ActionResult<BaseResponse<IEnumerable<SelectlistResult>>>> GetSelectableCargoes([FromQuery] GetSelectableCargoesQuery request) =>
            await Mediator.Send(request);

        [HttpPost]
        public async Task<ActionResult<BaseResponse>> CreateCargo([FromBody] CreateCargoCommand request) =>
            await Mediator.Send(request);

        [HttpPut]
        public async Task<ActionResult<BaseResponse>> UpdateCargo([FromBody] UpdateCargoCommand request) =>
            await Mediator.Send(request);

        [HttpDelete("{Id}")]
        [ProducesResponseType(typeof(BaseResponse), 200)]
        [ProducesResponseType(typeof(BaseResponse), 404)]
        public async Task<ActionResult<BaseResponse>> DeleteCargo([FromBody] DeleteCargoCommand request) =>
            await Mediator.Send(request);
    }
}
