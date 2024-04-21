﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapDat.Application.Models.GeoObjects
{
    public class GeometryViewModel
    {
        public string Type { get; set; } = String.Empty;
        public List<string[]> Coordinates { get; set; } = new List<string[]>();
    }
}
