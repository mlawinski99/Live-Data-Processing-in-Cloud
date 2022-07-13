using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LiveDataProcessing
{
    public static class APICaller
    {
        public static  readonly string apiKey = "apiKey";
        private static readonly HttpClient client = new HttpClient();
        public static async Task<(string, string)> ApiCall(string query)
        {
            try
            {
                var response = await client.GetAsync(query);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(responseBody);
                string temp = (string)o["current"]["temp_c"];
                string date = (string)o["current"]["last_updated"] + ":00";
                DateTime dateTime = DateTime.Parse(date);
                string parsedDateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
                return (temp, parsedDateTime);
            }
            catch (Exception e)
            {
                Console.WriteLine("Expection !");
                return (null, null);
            }

        }
    }
}
