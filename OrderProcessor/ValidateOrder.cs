using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace OrderProcessor
{
    public static class Validate
    {
        [Disable()]
        [FunctionName(nameof(ValidateOrder))]
        public static async Task ValidateOrder(
            [ServiceBusTrigger("incoming-orders", Connection = "AzureServiceBusConnectionString")]string Sorder, 
            [ServiceBus("validated-orders", Connection = "AzureServiceBusConnectionString", EntityType = Microsoft.Azure.WebJobs.ServiceBus.EntityType.Topic)] IAsyncCollector<string> validatedOrder,
            ILogger log)
        {
            JObject order = JObject.Parse(Sorder);
            log.LogInformation($"Got incoming order: {order}");
            log.LogInformation("Validating...");
            await Task.Delay(100);
            Guid validationId = Guid.NewGuid();
            log.LogInformation($"Order was valid. Validation ID {validationId}");
            order["validationId"] = validationId;
            await validatedOrder.AddAsync(order.ToString());
        }
    }
}
