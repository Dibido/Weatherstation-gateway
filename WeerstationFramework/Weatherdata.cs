using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeerstationFramework
{
    public class Weatherdata
    {
        public String name { get; set; }
        public decimal temp { get; set; }
        public decimal lux { get; set; }
        public DateTime time { get; set; }
    }
}