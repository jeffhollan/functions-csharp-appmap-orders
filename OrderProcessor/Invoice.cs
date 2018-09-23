using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace OrderProcessor
{
    public static class InvoiceClass
    {
        private static Random rnd = new Random();
        [Disable()]
        [FunctionName(nameof(Invoice))]
        public static async Task Invoice(
            [ServiceBusTrigger("validated-orders", "charge", Connection = "AzureServiceBusConnectionString")]string Sorder, 
            [ServiceBus("charged-orders", Connection = "AzureServiceBusConnectionString", EntityType = Microsoft.Azure.WebJobs.ServiceBus.EntityType.Queue)] IAsyncCollector<string> chargedOrder,
            ILogger log)
        {
            JObject order = JObject.Parse(Sorder);
            log.LogInformation($"Got validated order to charge: {order}");
            log.LogInformation("Charging the customer");
            if(rnd.Next(0, 10) == 0)
            {
                throw new InvalidOperationException("Customer card rejected payment");
            }
            await Task.Delay(rnd.Next(300, 1000));
            log.LogInformation("Charged successfully");
            await chargedOrder.AddAsync(order.ToString());
        }
    }
}
