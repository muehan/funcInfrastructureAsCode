using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Commands;
using System;

namespace funcInfrastructureAsCode.Functions.Functions
{
    public static class RegenerateTerraformFile
    {
        [FunctionName("RegenerateTerraformFile")]
        [return: Queue("terraformTrigger", Connection = "AzureWebJobsStorage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] CreateVirtualMachineCommand command,
            ILogger log)
        {
            log.LogInformation("RegenerateTerraformFile trigger");

            return new OkObjectResult(new {});
        }
    }
}
