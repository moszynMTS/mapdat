using MapDat.Domain.Common;
using MediatR;
using MapDat.Persistance.Services;
using MapDat.Domain.Entities;
using MongoDB.Driver.GeoJsonObjectModel;
using MongoDB.Bson;

namespace MapDat.Application.Features.Common.Queries
{
    public abstract class BaseQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, BaseResponse<TResponse>>
        where TRequest : IRequest<BaseResponse<TResponse>>
        where TResponse : class
    {
        protected readonly IWojewodztwaService _wojewodztwaService;
        protected BaseQueryHandler(IWojewodztwaService wojewodztwaService)
        {
            _wojewodztwaService = wojewodztwaService;
        }
        public abstract Task<BaseResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
        public string ToGeoJson(MyGeoObject entity)
        {
            var coordinatesString = entity.Geometry.Coordinates.ToString();
            string result = coordinatesString.Substring(1, coordinatesString.Length - 2);
            return $"{{" +
                $"\"type\": \"{entity.Type}\"," +
                $"  \"geometry\": " +
                $"{{   \"type\": \"{entity.Geometry.Type}\"," +
                $"    \"coordinates\": {result}" +
                $"  }}," +
                $"  \"properties\": {{" +
                $"    \"name\": \"{entity.Properties.Name}\"" +
                $"  }}" +
                $"}}";
        }
    }
}
