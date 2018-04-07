using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WeerstationFramework.Controllers
{
    public class GroetController : ApiController
    {
        public string get(string id)
        {
            return "Hoi " + id;
        }
    }
}
