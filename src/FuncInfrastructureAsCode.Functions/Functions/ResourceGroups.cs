using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using System.Linq;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Models;
using System.Collections.Generic;

namespace FuncInfrastructureAsCode.Functions
{
    public static class ResourceGroups
    {
        [FunctionName("ResourceGroups")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Table("RecourceGroup", Connection = "AzureWebJobsStorage")] TableClient resourceGroupTable,
            ILogger log)
        {
            var groups = resourceGroupTable.Query<ResourceGroup>().ToList();

            var results = new List<ResourceGroupViewModel>();

            groups.ForEach(group =>
            {
                results
                    .Add(
                        new ResourceGroupViewModel {
                            Name = group.Name,
                            LocalName = group.LocalName,
                            Location = group.Location
                        }
                    );
            });

            return new OkObjectResult(results);
        }
    }
}
