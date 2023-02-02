using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using funcInfrastructureAsCode.Functions.DbModels;

namespace funcInfrastructureAsCode.Functions.Functions
{
    public static class AddResourceGroup
    {
        [FunctionName("AddResourceGroup")]
        [return: Queue("terraformTrigger", Connection = "AzureWebJobsStorage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Table("RecourceGroup", Connection = "AzureWebJobsStorage")] IAsyncCollector<ResourceGroup> resourceTable,
            ILogger log)
        {
            log.LogInformation("AddResourceGroup trogger");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            log.LogInformation($"name {data?.name} | location {data?.location}");

            var resourceGroup = new ResourceGroup
            {
                Name = data?.name,
                Location = data?.location,
                RowKey = Guid.NewGuid().ToString("n"),
                PartitionKey = data?.location
            };

            await resourceTable.AddAsync(resourceGroup);

            // return new OkObjectResult(JsonConvert.SerializeObject(resourceGroup));
            return new OkObjectResult(resourceGroup);
        }
    }
}
