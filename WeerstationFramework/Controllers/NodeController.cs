using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WeerstationFramework.Controllers
{
    public class NodeController : ApiController
    {
        public string get(string id)
        {
            System.Diagnostics.Debug.WriteLine(id);
            System.Diagnostics.Debug.WriteLine(HttpContext.Current.Request.UserHostAddress);
            System.Diagnostics.Debug.WriteLine(HttpContext.Current.Request.UserHostAddress);
            return "OK";
        }
    }
}
