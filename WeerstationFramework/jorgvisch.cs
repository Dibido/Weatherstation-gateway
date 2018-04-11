using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;

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
            Token.GetToken();
            HttpWebRequest request = WebRequest.CreateHttp("http://iot.jorgvisch.nl/api/greetings");
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + Token.token);

            WebResponse response = request.GetResponse();

            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);
            string html = reader.ReadToEnd();

            Console.WriteLine(html);
            Console.WriteLine();
        }

        public static HttpResponseMessage getData(int count)
        {
            Token.GetToken();
            HttpWebRequest request = WebRequest.CreateHttp("http://iot.jorgvisch.nl/api/weather/" + count.ToString());
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + Token.token);

            WebResponse httpresponse = request.GetResponse();

            Stream data = httpresponse.GetResponseStream();
            StreamReader reader = new StreamReader(data);
            string json = reader.ReadToEnd();

            TempData[] values = JsonConvert.DeserializeObject<TempData[]>(json);

            List<String> tableContents = new List<string>();
            String header = "<tr><th>Weatherstation</th><th>Timestamp</th><th>Temperature</th><th>Illuminance</th></tr>";
            tableContents.Add(header);
            foreach (TempData d in values)
            {
                var trs = String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>", d.Weatherstation, d.Timestamp, d.Temperature, d.Illuminance);
                tableContents.Add(trs);
            }

            var combinedString = String.Join("", tableContents.ToArray());
            var table = "<!DOCTYPE html><html><head><title>Connected nodes</title></head><body><table>" + combinedString + "</table></body></html>";
            var response = new HttpResponseMessage();
            response.Content = new StringContent(table);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");

            return response;
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
            String json = "{\"Weatherstation\": \"" + name + "\",\"Timestamp\":\"" + time + "\",\"Temperature\": " + temperature + ",\"Illuminance\": " + light + "}";
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
