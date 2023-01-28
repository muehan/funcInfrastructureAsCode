using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace funcInfrastructureAsCode
{
    public class GenerateTerraform
    {
        [FunctionName("GenerateTerraform")]
        public void Run(
            [QueueTrigger("terraformTrigger", Connection = "AzureWebJobsStorage")] string myQueueItem,
            ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
