using MapDat.Application.Features.RSPO.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDat.Application.Models.RSPO
{
    public class InfoViewModel
    {
        public InfoViewModel()
        {
            
        }
        public InfoViewModel(GetSchoolsQuery query)
        {
            Wojewodztwo = query.Wojewodztwo;
            Powiat = query.Powiat;
            Gmina = query.Gmina;
        }
        public string Wojewodztwo { get; set; } = null!;
        public string? Powiat { get; set; }
        public string? Gmina { get; set; }
        public int Count { get; set; }
        public string Name { get; set; } = null!;
    }
}
