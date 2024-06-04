﻿using MapDat.Application.Features.Common;
using MapDat.Application.Features.Common.Queries;
using MapDat.Application.Models.RSPO;
using MapDat.Domain.Entities;
using MapDat.Persistance.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MapDat.Application.Features.RSPO.Queries
{
    public class GetInfoQueryHandler : BaseQueryHandler<GetInfoQuery, IEnumerable<InfoViewModel>>
    {
        public GetInfoQueryHandler(IMongoService mongoService)
            : base(mongoService) { }

        public async override Task<BaseResponse<IEnumerable<InfoViewModel>>> Handle(GetInfoQuery request, CancellationToken cancellationToken)
        {
            var type = typeof(WojewodztwoPropertiesObject);
            var result = new List<InfoViewModel>();
            foreach (var id in request.Wojewodztwa)
            {
                var info = _mongoService.GetInfo(id);
                var item = new InfoViewModel();
                item.WojewodztwoId = id;
                foreach (var subject in request.Subjects)
                {
                    var property = type.GetProperty(subject);
                    if (property != null)
                    {
                        var value = property.GetValue(info);
                        if (value != null)
                            item.Data.Add(new DataModel { Subject = subject, Count = value.ToString() });
                    }
                }
                if (request.Subjects.Contains("Szkoły"))
                {
                    item.Data.Add(new DataModel { Subject = "Szkoły", Count = await GetSchoolData(id, null, null) });
                }
                result.Add(item);
            }
            if (request.Subjects.Contains("Szkoły"))
            {
                foreach (var id in request.Powiaty)
                {
                    var item = new InfoViewModel();
                    item.PowiatId = id;
                    item.Data.Add(new DataModel { Subject = "Szkoły", Count = await GetSchoolData(null, id, null) });
                    result.Add(item);
                }
                foreach (var id in request.Gminy)
                {
                    var item = new InfoViewModel();
                    item.GminaId = id;
                    item.Data.Add(new DataModel { Subject = "Szkoły", Count = await GetSchoolData(null, null, id) });
                    result.Add(item);
                }
            }

            return new BaseResponse<IEnumerable<InfoViewModel>>(statusCode: HttpStatusCode.OK, content: result);
        }
        public string GetObject(string? wojewodztwoId, string? powiatId, string? gminaId)
        {
            var result = "";
            if (wojewodztwoId != null)
                result = _mongoService.GetWojewodztwo(wojewodztwoId).Properties.Name;
            if (powiatId != null)
            {
                var item = _mongoService.GetPowiat(powiatId);
                result = $"{item.Properties.Wojewodztwo};{item.Properties.Name};";
            }
            if (gminaId != null)
            {
                var item = _mongoService.GetGmina(gminaId);
                result = $"{""};{item.Properties.Powiat};{item.Properties.Name}";
            }
            return result;
        }
        public async Task<string> GetSchoolData(string? wojewodztwoId, string? powiatId, string? gminaId)
        {
            var value = GetObject(wojewodztwoId, powiatId, gminaId);
            var list = value.Split(';');
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "https://api-rspo.mein.gov.pl/api/placowki/?";
                    string propertiesWojewodztwo = $"wojewodztwo_nazwa={list[0]}";
                    string propertiesPowiat = $"powiat_nazwa={list[1]}";
                    string propertiesGmina = $"gmina_nazwa={list[2]}";
                    if (list[0] != "")
                        url += $"&{propertiesWojewodztwo}";
                    if (list[1] != "")
                        url += $"&{propertiesPowiat}";
                    if (list[2] != "")
                        url += $"&{propertiesGmina}";

                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject jsonResponse = JObject.Parse(responseBody);
                    int totalItems = jsonResponse["hydra:totalItems"].Value<int>();

                    return totalItems.ToString();
                }
                catch (HttpRequestException e)
                {
                    return "rspo request failed";
                }
                catch (Exception e)
                {
                    return "rspo request failed";
                }
            }
        }
    }
}
