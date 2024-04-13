using MapDat.Application.Features.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MapDat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private ISender? _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        protected async Task<ActionResult<BaseResponse<TResponse>>> ExecuteRequest<TResponse>(IRequest<BaseResponse<TResponse>> request)
        {
            var result = await Mediator.Send(request);
            return result.StatusCode switch
            {
                HttpStatusCode.OK => (ActionResult<BaseResponse<TResponse>>)Ok(result),
                HttpStatusCode.Created => (ActionResult<BaseResponse<TResponse>>)Created("", result),
                HttpStatusCode.Accepted => (ActionResult<BaseResponse<TResponse>>)Accepted(result),
                HttpStatusCode.NotFound => (ActionResult<BaseResponse<TResponse>>)NotFound(result),
                HttpStatusCode.BadRequest => (ActionResult<BaseResponse<TResponse>>)BadRequest(result),
                _ => (ActionResult<BaseResponse<TResponse>>)BadRequest(result),
            };
        }

        protected async Task<ActionResult<BaseResponse>> ExecuteRequest(IRequest<BaseResponse> request)
        {
            var result = await Mediator.Send(request);
            return result.StatusCode switch
            {
                HttpStatusCode.OK => (ActionResult<BaseResponse>)Ok(result),
                HttpStatusCode.Created => (ActionResult<BaseResponse>)Created("", result),
                HttpStatusCode.Accepted => (ActionResult<BaseResponse>)Accepted(result),
                HttpStatusCode.NotFound => (ActionResult<BaseResponse>)NotFound(result),
                HttpStatusCode.BadRequest => (ActionResult<BaseResponse>)BadRequest(result),
                _ => (ActionResult<BaseResponse>)BadRequest(result),
            };
        }
        protected async Task<ActionResult> ExecuteFileReturnQuery(IRequest<byte[]> request, string contentType)
        {
            //var result = await command.Invoke();
            var result = await Mediator.Send(request);
            return File(result, contentType);
        }
    }
}
