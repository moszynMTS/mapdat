﻿using MapDat.Application.Features.Common;
using MapDat.Application.Models.RSPO;
using MapDat.Application.Models.Wojewodztwa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDat.Application.Features.RSPO.Queries
{
    public class GetSchoolsQuery : BaseRequest<InfoViewModel>
    {
        public string Wojewodztwo { get; set; } = null!;
        public string? Powiat { get; set; }
        public string? Gmina { get; set; }
    }
}