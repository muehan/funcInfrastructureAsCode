using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace funcInfrastructureAsCode.Functions.Functions
{
    public static class RegenerateTerraformFile
    {
        [FunctionName("RegenerateTerraformFile")]
        // [return: Queue("terraformTrigger", Connection = "AzureWebJobsStorage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] dynamic body,
            [Queue("terraformTrigger", Connection = "AzureWebJobsStorage")] IAsyncCollector<string> terraformTriggerQueue,
            ILogger log)
        {
            string requestId = body.requestId;

            log.LogInformation($"RegenerateTerraformFile trigger with {requestId}");

            await terraformTriggerQueue
                .AddAsync(
                    requestId);

            return new OkObjectResult(new { });
        }
    }
}
