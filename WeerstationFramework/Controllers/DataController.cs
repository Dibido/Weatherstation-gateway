
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WeerstationFramework.Controllers
{
    public class DataController : ApiController
    {
        public String get()
        {
            return "test";
        }

        public HttpResponseMessage get(int count)
        {
            //Get the count row from the remote server.
            return jorgvisch.getData(count);
        }
    }
}
