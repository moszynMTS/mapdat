using MapDat.Application.Features.Common.Queries;
using MapDat.Application.Features.Common;
using MapDat.Application.Features.Powiaty.Queries;
using MapDat.Application.Models.Wojewodztwa;
using MapDat.Persistance.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MapDat.Application.Models.RSPO;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Newtonsoft.Json.Linq;

namespace MapDat.Application.Features.RSPO.Queries
{
    public class GetSchoolsQueryHandler : BaseQueryHandler<GetSchoolsQuery, InfoViewModel>
    {
        public GetSchoolsQueryHandler(IMongoService mongoService)
            : base(mongoService) { }
        public async override Task<BaseResponse<InfoViewModel>> Handle(GetSchoolsQuery request, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "https://api-rspo.mein.gov.pl/api/placowki/?";
                    string propertiesWojewodztwo = $"wojewodztwo_nazwa={request.Wojewodztwo}";
                    string propertiesPowiat = $"powiat_nazwa={request.Powiat}";
                    string propertiesGmina = $"gmina_nazwa={request.Gmina}";
                    url += propertiesWojewodztwo;
                    if(request.Powiat!=null)
                        url += $"&{propertiesPowiat}";
                    if (request.Gmina != null)
                        url += $"&{propertiesGmina}";

                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jsonResponse = JObject.Parse(responseBody);
                    int totalItems = jsonResponse["hydra:totalItems"].Value<int>();

                    var result = new InfoViewModel(request);
                    result.Count= totalItems;
                    result.Name = "placowki szkolne";

                    return new BaseResponse<InfoViewModel>(statusCode: HttpStatusCode.OK, content: result);
                }
                catch (HttpRequestException e)
                {
                    // Obsłuż wyjątki związane z zapytaniem HTTP
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                    return new BaseResponse<InfoViewModel>(statusCode: HttpStatusCode.BadRequest, content: null, error:"rspo request failed");
                }
                catch (Exception e)
                {
                    // Obsłuż wyjątki związane z zapytaniem HTTP
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message :{0} ", e.Message);
                    return new BaseResponse<InfoViewModel>(statusCode: HttpStatusCode.BadRequest, content: null, error:"rspo request failed");
                }
            }
        }
    }
}

