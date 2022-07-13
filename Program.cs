using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json.Linq;

namespace LiveDataProcessing
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<string> cityList = new List<string> { "Warszawa", "Pekin", "Washington", "Brasilia" };
            DataHandler dataHandler = new DataHandler(cityList, APICaller.apiKey);
            DataStreamer dataStreamer = new DataStreamer();
            while (true)
            {
                for (int i = 0; i < dataHandler.QueryList.Count; i++)
                {
                    (string temp, string date) apiResponse = await APICaller.ApiCall(dataHandler.QueryList[i]);
                    string message = "{\"temp\":" + apiResponse.temp + ", \"location\":\"" + cityList[i] + "\", \"mensurationLocalTime\": \"" + apiResponse.date + "\"}";
                    Console.WriteLine(message);
                    await dataStreamer.SendMessagesToEventHub(message);
                }
                Thread.Sleep(900000); //15 minutes
            }
        }
    }
}
