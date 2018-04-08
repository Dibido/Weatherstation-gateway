using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WeerstationFramework.Controllers
{
    public class TimeController : ApiController
    {
        public String get()
        {
            return DateTime.Now.ToString("yyyy:M:d:HH:mm:ss");
        }
    }
}
