using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace OrderProcessor
{
    public static class EmailClass
    {
        private static Random rnd = new Random();
        [Disable()]
        [FunctionName(nameof(Email))]
        public static async Task Email(
            [ServiceBusTrigger("charged-orders", Connection = "AzureServiceBusConnectionString")]string Sorder, 
            ILogger log)
        {
            JObject order = JObject.Parse(Sorder);
            log.LogInformation($"Got charged order to email: {order}");
            log.LogInformation("Emailing customer");
            if (rnd.Next(0, 100) == 0)
            {
                throw new InvalidOperationException("Customer email address doesn't exist: fakeemail@fake.com");
            }
            await Task.Delay(rnd.Next(500, 1000));
            log.LogInformation("Email sent");
        }
    }
}
