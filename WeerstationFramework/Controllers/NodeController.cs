using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace WeerstationFramework.Controllers
{
    public class NodeController : ApiController
    {
        [HttpPost]
        public void post([FromBody]String node, [FromBody]float min, [FromBody]float max)
        {
            System.Diagnostics.Debug.WriteLine(node + ":" + min + ":" + max);
            //lookup ip
            String ip = iptable.table.FirstOrDefault(x => x.Key == node).Value;
            //send post request with the min and max

            //read out the response
        }

        public HttpResponseMessage Get()
        {
            var trs = iptable.table.Select(a => String.Format("<tr><td>{0}</td><td>{1}</td></tr>", a.Key, a.Value));
            var tableContents = String.Concat(trs);
            var table = "<!DOCTYPE html><html><head><title>Connected nodes</title></head><body><table>" + tableContents + "</table></body></html>";
            var response = new HttpResponseMessage();
            response.Content = new StringContent(table);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        public string get(string id)
        {
            System.Diagnostics.Debug.WriteLine(id);
            System.Diagnostics.Debug.WriteLine(HttpContext.Current.Request.UserHostAddress);
            //If the name is, update the ip.
            if(iptable.table.ContainsKey(id))
            {
                iptable.table[id] = HttpContext.Current.Request.UserHostAddress;
            }
            //If the name and ip are not in the table add them.
            else if (!iptable.table.Contains(new KeyValuePair<string, string>(id, HttpContext.Current.Request.UserHostAddress)))
            {
                iptable.table.Add(new KeyValuePair<string, string>(id, HttpContext.Current.Request.UserHostAddress));
            }
            return "OK";
        }
    }
}
