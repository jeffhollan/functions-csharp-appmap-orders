using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace OrderProcessor
{
    public static class AppendClass
    {
        private static Random rnd = new Random();
        [Disable()]
        [FunctionName(nameof(Append))]
        public static async Task Append(
            [ServiceBusTrigger("validated-orders", "append", Connection = "AzureServiceBusConnectionString")]string Sorder, 
            ILogger log)
        {
            JObject order = JObject.Parse(Sorder);
            log.LogInformation($"Got validated order to append {order}");
            log.LogInformation("Appending order to CosmosDB");
            //if(rnd.Next(0, 10) == 0)
            //{
            //    throw new InvalidOperationException("CosmosDB item with this ID already exists");
            //}
            await Task.Delay(rnd.Next(0, 1000));
            log.LogInformation("Appended successfully");
        }
    }
}
