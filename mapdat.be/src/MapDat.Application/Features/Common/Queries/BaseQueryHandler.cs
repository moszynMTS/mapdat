using MediatR;
using MapDat.Persistance.Services;
using MapDat.Domain.Entities;
using MongoDB.Bson;

namespace MapDat.Application.Features.Common.Queries
{
    public abstract class BaseQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest, BaseResponse<TResponse>>
        where TRequest : IRequest<BaseResponse<TResponse>>
        where TResponse : class
    {
        protected readonly IMongoService _mongoService;
        protected BaseQueryHandler(IMongoService mongoService)
        {
            _mongoService = mongoService;
        }
        public abstract Task<BaseResponse<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
        public string ToGeoJson(MyGeoObject<WojewodztwoPropertiesObject> entity)
        {
            return entity.ToJson();
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
