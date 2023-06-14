using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using funcInfrastructureAsCode.Functions.Commands;

namespace funcInfrastructureAsCode.Functions.Functions
{
    public static class RegenerateTerraformFile
    {
        [FunctionName("RegenerateTerraformFile")]
        [return: Queue("terraformTrigger", Connection = "AzureWebJobsStorage")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] CreateVirtualMachineCommand command,
            ILogger log)
        {
            log.LogInformation("RegenerateTerraformFile trigger");

            return new OkObjectResult(new {});
        }
    }
}
