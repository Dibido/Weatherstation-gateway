using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;

namespace WeerstationFramework.Controllers
{
    public class NodeController : ApiController
    {
        [HttpPost]
        public void post([FromBody]Nodedata nodedata)
        {
            System.Diagnostics.Debug.WriteLine(nodedata.name + ":" + nodedata.min + ":" + nodedata.max);
            
            //lookup ip
            String ip = iptable.table.FirstOrDefault(x => x.Key == nodedata.name).Value;
            if (string.IsNullOrEmpty(ip)) // If there is no IP available for a node with the given name
            {
                System.Diagnostics.Debug.WriteLine("return, no match");
                return;
            }
            System.Diagnostics.Debug.WriteLine(ip);
            //send post request with the min and max
            HttpWebRequest request = WebRequest.CreateHttp("http://" + ip + "/temp");
            request.Method = "POST";
            request.ContentType = "application/json";
            String tempMin = nodedata.min.ToString();
            String tempMax = nodedata.max.ToString();
            String json = "{\"min\":" + tempMin + ",\"max\":" + tempMax + "}";
            System.Diagnostics.Debug.WriteLine(ip);
            System.Diagnostics.Debug.WriteLine("ContentLength: " + json.Length);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(json);


            request.ContentLength = data.Length;
            Stream requestdata = request.GetRequestStream();
            requestdata.Write(data, 0, data.Length);
            requestdata.Close();

           // StreamWriter content = new StreamWriter(requestdata);

            System.Diagnostics.Debug.WriteLine(json);
            /*
            content.WriteAsync(json);
            content.Close();
            requestdata.Close();

            WebResponse response = request.GetResponse();
            Stream responseData = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseData);
            string html = reader.ReadToEnd();

            System.Diagnostics.Debug.WriteLine(html);
            */
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
            // If the IP is also not in the table:
            else if (iptable.table.FirstOrDefault(x => x.Value == HttpContext.Current.Request.UserHostAddress).Key == null)
            {
                iptable.table.Add(new KeyValuePair<string, string>(id, HttpContext.Current.Request.UserHostAddress));
            }
            //If the IP IS already registred in the table, change its name by remove/adding.
            else
            {
                iptable.table.Remove(iptable.table.FirstOrDefault(x => x.Value == HttpContext.Current.Request.UserHostAddress).Key);
                iptable.table.Add(new KeyValuePair<string, string>(id, HttpContext.Current.Request.UserHostAddress));
            }
            System.Diagnostics.Debug.WriteLine("OK");
            return "OK";
        }
    }
}
