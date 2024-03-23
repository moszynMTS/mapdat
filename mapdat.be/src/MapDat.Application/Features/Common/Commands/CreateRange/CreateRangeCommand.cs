using MediatR;

namespace MapDat.Application.Features.Common.Commands.CreateRange
{
    public class CreateRangeCommand<TCommand, TResponse> : IRequest<BaseResponse<IEnumerable<TResponse>>>
    {
        public IEnumerable<TCommand> Commands { get; set; } = new List<TCommand>();
        public bool AddHistory { get; set; } = false;
    }
}
