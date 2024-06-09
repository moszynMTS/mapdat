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
        public string? WojewodztwoId { get; set; }
        public string? PowiatId { get; set; }
        public string? GminaId { get; set; }
        public List<DataModel> Data { get; set; } = new List<DataModel>();
        
    }
    public class DataModel
    {
        public string? Count { get; set; }
        public string Subject { get; set; } = null!;
    }
}
