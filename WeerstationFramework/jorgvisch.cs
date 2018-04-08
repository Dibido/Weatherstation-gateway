using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace WeerstationFramework
{
    public static class jorgvisch
    {
        //2018-03-16T10:18:31.7452075+01:00

        public static void getGreetingUnauth()
        {
            HttpWebRequest request = WebRequest.CreateHttp("http://iot.jorgvisch.nl/api/greetings");
            WebResponse response = request.GetResponse();

            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);
            string html = reader.ReadToEnd();

            Console.WriteLine(html);
            Console.WriteLine();
        }

        public static void getGreetingAuth(string token)
        {
            HttpWebRequest request = WebRequest.CreateHttp("http://iot.jorgvisch.nl/api/greetings");
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);

            WebResponse response = request.GetResponse();

            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);
            string html = reader.ReadToEnd();

            Console.WriteLine(html);
            Console.WriteLine();
        }

        public static void sendSensorData(String name, DateTime time, decimal temp, decimal lux)
        {
            Token.GetToken();
            HttpWebRequest request = WebRequest.CreateHttp("http://iot.jorgvisch.nl/api/Weather");
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + Token.token);
            request.Method = "POST";
            request.ContentType = "application/json";
            Stream requestdata = request.GetRequestStream();
            StreamWriter content = new StreamWriter(requestdata);

            String temperature = temp.ToString().Replace(',', '.');
            String light = lux.ToString().Replace(',', '.');
            String json = "{\"Weatherstation\": \"" + name + "\",\"Timestamp\":\"" + DateTime.Now + "\",\"Temperature\": " + temperature + ",\"Illuminance\": " + light + "}";
            System.Diagnostics.Debug.WriteLine(json);
            Console.WriteLine(json);

            content.Write(json);
            content.Close();

            WebResponse response = request.GetResponse();
            Stream responseData = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseData);
            string html = reader.ReadToEnd();
        }
    }
}