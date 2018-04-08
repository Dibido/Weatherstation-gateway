using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace WeerstationFramework.Controllers
{
    public class WeatherController : ApiController
    {
        //function to post the weather data
        //weather is formatted in json
        [HttpPost]
        public void Post([FromBody]Weatherdata weather)
        {
            System.Diagnostics.Debug.WriteLine("sending values to server:");
            System.Diagnostics.Debug.WriteLine(weather.temp);
            System.Diagnostics.Debug.WriteLine(weather.lux);
            //System.Diagnostics.Debug.WriteLine(weather.time.ToString());
            //dynamic values = JsonConvert.DeserializeObject(weather);
            //Get the ip
            String ipaddress = HttpContext.Current.Request.UserHostAddress;
            //Get the associated node name
            String name = iptable.table.FirstOrDefault(x => x.Value == ipaddress).Key;
            //If the node is not in the iptable, give an error.
            if (string.IsNullOrEmpty(name))
            {
                System.Diagnostics.Debug.WriteLine("Node name is not known for ip" + ipaddress + ".");
            }
            System.Diagnostics.Debug.WriteLine("sending values to server");
            jorgvisch.sendSensorData(jorgvisch.getToken(), name, DateTime.Now, weather.temp, weather.lux);
        }
    }
}
