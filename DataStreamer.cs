using Microsoft.Azure.EventHubs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveDataProcessing
{
    public class DataStreamer
    {
        private readonly EventHubClient eventHubClient;
        private readonly string connectionString; //connection String to Azure
        private readonly string eventHubName;
        private readonly EventHubsConnectionStringBuilder connectionStringBuilder;
        public DataStreamer()
        {
            this.connectionString = "connectionString";
            this.eventHubName = "eventHubName";
            this.connectionStringBuilder = new EventHubsConnectionStringBuilder(connectionString)
            {
                EntityPath = eventHubName
            };
            this.eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
        }
        public async Task SendMessagesToEventHub(string message)
        {
            try
            {
                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
                Console.WriteLine($"Sent message: '{message}'");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
            }
            await Task.Delay(10);
            Console.WriteLine($"{message} messages sent.\n");
        }
    }
}
