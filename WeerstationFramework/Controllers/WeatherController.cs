using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WeerstationFramework.Controllers
{
    public class WeatherController : ApiController
    {
        //function to post the weather data
        //weather is formatted in json
        [HttpPost]
        public void Post([FromBody]Weatherdata weather)
        {
            System.Diagnostics.Debug.WriteLine("TEST");
            if (weather != null)
            {
                System.Diagnostics.Debug.WriteLine("sending values to server:");
                System.Diagnostics.Debug.WriteLine(weather.temp);
                System.Diagnostics.Debug.WriteLine(weather.lux);
                System.Diagnostics.Debug.WriteLine(weather.time);
                //Get the ip
                String ipaddress = HttpContext.Current.Request.UserHostAddress;
                String name = iptable.table.FirstOrDefault(x => x.Value == ipaddress).Key;
                //If there is no node with the given IP in the iptable:
                if (string.IsNullOrEmpty(name))
                {
                    //Add the node into the table
                    System.Diagnostics.Debug.WriteLine("Node name is not known for ip" + ipaddress + ".");
                    //If the name exists, update the ip.
                    if (iptable.table.ContainsKey(weather.name))
                    {
                        iptable.table[weather.name] = HttpContext.Current.Request.UserHostAddress;
                    }
                    //If the name and ip are not in the table add them.
                    else
                    { 
                        iptable.table.Add(new KeyValuePair<string, string>(weather.name, HttpContext.Current.Request.UserHostAddress));
                    }
                }
                //If the IP exists, but the given name is different then the name in the table.
                else if (name != weather.name)
                {
                    iptable.table.Remove(iptable.table.FirstOrDefault(x => x.Value == HttpContext.Current.Request.UserHostAddress).Key);
                    System.Diagnostics.Debug.WriteLine(iptable.table);
                    iptable.table.Add(new KeyValuePair<string, string>(weather.name, HttpContext.Current.Request.UserHostAddress));
                }
                //Get the associated node name
                name = iptable.table.FirstOrDefault(x => x.Value == ipaddress).Key;
                System.Diagnostics.Debug.WriteLine("sending values to server");
                jorgvisch.sendSensorData(name, weather.time, weather.temp, weather.lux);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Weather was null");
            }
        }
    }
}
