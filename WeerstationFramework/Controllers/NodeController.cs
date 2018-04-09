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
            //If the name and ip are not in the table add them.
            if (!iptable.table.Contains(new KeyValuePair<string, string>(id, HttpContext.Current.Request.UserHostAddress)))
            {
                iptable.table.Add(new KeyValuePair<string, string>(id, HttpContext.Current.Request.UserHostAddress));
            }//If the name is, update the ip.
            else if(iptable.table.ContainsKey(id))
            {
                iptable.table[id] = HttpContext.Current.Request.UserHostAddress;
            }
            return "OK";
        }
    }
}
