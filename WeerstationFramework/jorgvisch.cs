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
        private static string getToken()
        {
            var request = WebRequest.CreateHttp("http://iot.jorgvisch.nl/token");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            Console.WriteLine(request);

            Stream data = request.GetRequestStream();
            StreamWriter content = new StreamWriter(data);
            content.Write("grant_type=password&username=HS.Dokter%40student.han.nl&password=P@ssw0rd");
            content.Close();

            WebResponse response = request.GetResponse();
            var response_data = response.GetResponseStream();
            var reader = new StreamReader(response_data);
            var json = reader.ReadToEnd();

            dynamic values = JsonConvert.DeserializeObject(json);
            String token = values.access_token;

            return token;
        }

        private static void getGreetingUnauth()
        {
            HttpWebRequest request = WebRequest.CreateHttp("http://iot.jorgvisch.nl/api/greetings");
            WebResponse response = request.GetResponse();

            Stream data = response.GetResponseStream();
            StreamReader reader = new StreamReader(data);
            string html = reader.ReadToEnd();

            Console.WriteLine(html);
            Console.WriteLine();
        }

        private static void getGreetingAuth(string token)
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

        private static void sendSensorData(String token, String name, DateTime time, float temp, float lux)
        {
            Console.WriteLine("{\"Weatherstation\": \"" + name + "\",\"Timestamp\": \"" + time.GetDateTimeFormats() + "\",\"Temperature\": " + temp.ToString() + ",\"Illuminance\": " + lux.ToString() + "}");
            HttpWebRequest request = WebRequest.CreateHttp("http://iot.jorgvisch.nl/api/Weather");
            request.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + token);
            request.Method = "POST";
            request.ContentType = "application/json";

            Console.WriteLine(request);

            Stream data = request.GetRequestStream();
            StreamWriter content = new StreamWriter(data);

            content.Close();

            WebResponse response = request.GetResponse();
        }
    }
}