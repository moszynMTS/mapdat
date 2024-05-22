using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Queries;
using MapDat.Application.Models.Wojewodztwa;
using MapDat.Domain.Entities;
using MapDat.Persistance.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Net;

namespace MapDat.Application.Features.Gminy.Queries
{
    public class GetGminyQueryHandler : BaseQueryHandler<GetGminyQuery, IEnumerable<GminyViewModel>>
    {
        public GetGminyQueryHandler(IMongoService mongoService) 
            : base(mongoService) { }
        public async override Task<BaseResponse<IEnumerable<GminyViewModel>>> Handle(GetGminyQuery request,CancellationToken cancellationToken)
        {
            var entities = await _mongoService.GetGminy(request.Powiat);
            var result = new List<GminyViewModel>();
            foreach (var entity in entities)
                result.Add(new GminyViewModel(entity));
            return new BaseResponse<IEnumerable<GminyViewModel>>(statusCode: HttpStatusCode.OK,content: result);
        }
    public async Task<BaseResponse<IEnumerable<GminyViewModel>>> test(GetGminyQuery request, CancellationToken cancellationToken)
        {
            var data = "{"+
"\"66378ed778d59e64a283a44e\":["+
	"\"6640f88c9fa55774df6e24a3\","+
	"\"6640f88c9fa55774df6e2705\","+
	"\"6640f88b9fa55774df6e221d\","+
    "\"6640f88b9fa55774df6e221c\"" +
    "]," +
"\"66378ed778d59e64a283a4fb\":["+
	"\"6640f88b9fa55774df6e20a4\","+
	"\"6640f88c9fa55774df6e28c7\","+
	"\"6640f88c9fa55774df6e26cd\","+
    "\"6640f88b9fa55774df6e21ea\"" +
    "]," +
"\"66378ed878d59e64a283a592\":["+
	"\"6640f88c9fa55774df6e2420\","+
	"\"6640f88c9fa55774df6e241f\","+
	"\"6640f88c9fa55774df6e2688\","+
	"\"6640f88c9fa55774df6e23cc\","+
	"\"6640f88c9fa55774df6e2432\","+
	"\"6640f88b9fa55774df6e202b\","+
	"\"6640f88b9fa55774df6e2164\","+
	"\"6640f88c9fa55774df6e255a\","+
	"\"6640f88b9fa55774df6e22d8\","+
	"\"6640f88c9fa55774df6e2634\","+
	"\"6640f88b9fa55774df6e2282\","+
    "\"6640f88c9fa55774df6e2647\"" +
    "]," +
"\"66378ed778d59e64a283a47e\":["+
	"\"6640f88c9fa55774df6e2426\","+
	"\"6640f88b9fa55774df6e21da\","+
	"\"6640f88c9fa55774df6e2561\","+
    "\"6640f88b9fa55774df6e21dc\"" +
	"]," +
"\"66378ed878d59e64a283a5b4\":[" +
	"\"6640f88b9fa55774df6e211e\"," +
	"\"6640f88c9fa55774df6e2385\"," +
	"\"6640f88c9fa55774df6e2384\"," +
	"\"6640f88c9fa55774df6e2383\"," +
	"\"6640f88b9fa55774df6e211f\"" +
	"]," +
"\"66378ed778d59e64a283a4f2\":[" +
	"\"6640f88c9fa55774df6e249d\"," +
	"\"6640f88b9fa55774df6e20cf\"," +
	"\"6640f88b9fa55774df6e1f98\"" +
	"]," +
"\"66378ed778d59e64a283a518\":[" +
	"\"6640f88c9fa55774df6e24fa\"," +
	"\"6640f88c9fa55774df6e262c\"," +
	"\"6640f88b9fa55774df6e2277\"," +
	"\"6640f88b9fa55774df6e2278\"," +
	"\"6640f88b9fa55774df6e200f\"," +
	"\"6640f88c9fa55774df6e250a\"," +
	"\"6640f88c9fa55774df6e276c\"," +
	"\"6640f88b9fa55774df6e21b6\"" +
	"]," +
"\"66378ed778d59e64a283a44e\":[" +
	"\"6640f88c9fa55774df6e25c7\"," +
	"\"6640f88c9fa55774df6e24a3\"," +
	"\"6640f88c9fa55774df6e2705\"," +
	"\"6640f88b9fa55774df6e221c\"," +
	"\"6640f88b9fa55774df6e221d\"" +
	"]," +
"\"66378ed778d59e64a283a4b5\":[" +
	"\"6640f88c9fa55774df6e24d8\"," +
	"\"6640f88c9fa55774df6e2695\"," +
	"\"6640f88c9fa55774df6e28e9\"," +
	"\"6640f88b9fa55774df6e2117\"," +
	"\"6640f88b9fa55774df6e21b9\"," +
	"\"6640f88b9fa55774df6e1fe0\"," +
	"\"6640f88c9fa55774df6e2855\"," +
	"\"6640f88b9fa55774df6e223f\"," +
	"\"6640f88c9fa55774df6e25f3\"" +
	"]," +
"\"66378ed778d59e64a283a51b\":[" +
	"\"6640f88b9fa55774df6e2302\"," +
	"\"6640f88c9fa55774df6e26b9\"," +
	"\"6640f88c9fa55774df6e28ea\"," +
	"\"6640f88c9fa55774df6e27e4\"," +
	"\"6640f88b9fa55774df6e2301\"," +
	"\"6640f88b9fa55774df6e209b\"," +
	"\"6640f88b9fa55774df6e21d5\"," +
	"\"6640f88b9fa55774df6e21d8\"" +
	"]}";
            var dictionary = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(data);

            foreach (var kvp in dictionary)
            {
                string powiatId = kvp.Key;
                List<string> gminyIds = kvp.Value;

                Console.WriteLine($"PowiatId: {powiatId}");

                // Find documents where _id is in the list of gminyIds
                var filter = Builders<GminaEntity>.Filter.In("_id", gminyIds.Select(id => new ObjectId(id)));
                var update = Builders<GminaEntity>.Update.Set(x => x.Properties.PowiatId, powiatId);

                _mongoService.getGmina().UpdateMany(filter, update);

            }
            return new BaseResponse<IEnumerable<GminyViewModel>>(statusCode: HttpStatusCode.OK,content: new List<GminyViewModel>());
        }
    }
}
