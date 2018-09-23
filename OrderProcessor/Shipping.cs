using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace OrderProcessor
{
    public static class ShippingClass
    {
        private static Random rnd = new Random();
        [Disable()]
        [FunctionName(nameof(Shipping))]
        public static async Task Shipping(
            [ServiceBusTrigger("validated-orders", "shipping", Connection = "AzureServiceBusConnectionString")]string Sorder, 
            ILogger log)
        {
            JObject order = JObject.Parse(Sorder);
            log.LogInformation($"Got validated order to ship: {order}");
            log.LogInformation("Creating shipping order");
            //if(rnd.Next(0, 10) == 0)
            //{
            //    throw new InvalidOperationException("CosmosDB item with this ID already exists");
            //}
            await Task.Delay(rnd.Next(500, 1000));
            log.LogInformation("Shipping order created successfully");
        }
    }
}
