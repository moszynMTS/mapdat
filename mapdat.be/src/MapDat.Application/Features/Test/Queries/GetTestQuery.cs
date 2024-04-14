using MapDat.Application.Features.Common;
using MapDat.Application.Models.Test;

namespace MapDat.Application.Features.Test.Queries
{
    public class GetTestQuery : BaseRequest<TestViewModel>
    {
        public string Name { get; set; } = null!;
    }
}
