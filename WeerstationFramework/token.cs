using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WeerstationFramework
{
    public class Token
    {

        public static String token;
        public static DateTime tokenExpiration;

        public static void GetToken()
        {
            //Check expiration time
            if(tokenExpiration > DateTime.Now)
            {
                return;
            }
            //Get new token
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

            System.Diagnostics.Debug.WriteLine(json);
            Console.WriteLine(json);

            dynamic values = JsonConvert.DeserializeObject(json);
            String t = values.access_token;

            double expiresIn = Convert.ToDouble(values.expires_in);
            DateTime expiration = DateTime.Now;
            tokenExpiration = expiration.AddSeconds(expiresIn);

            token = t;
        }
    }
}