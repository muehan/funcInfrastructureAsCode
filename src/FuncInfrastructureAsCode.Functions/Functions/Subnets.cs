using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using System.Linq;
using System.Collections.Generic;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Models;

namespace FuncInfrastructureAsCode.Functions
{
    public static class Subnets
    {
        [FunctionName("Subnet")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Table("Subnet", Connection = "AzureWebJobsStorage")] TableClient subnetTable,
            ILogger log)
        {
            var subnets = subnetTable.Query<Subnet>().ToList();

            var results = new List<SubnetViewModel>();

            subnets.ForEach(group =>
            {
                results
                    .Add(
                        new SubnetViewModel {
                            Name = group.Name,
                            LocalName = group.LocalName,
                            ResourceGroupName = group.ResourceGroupName,
                            AddressPrefixes = group.AddressPrefixes,
                            VirtualNetworkName = group.VirtualNetworkName
                        }
                    );
            });

            return new OkObjectResult(results);
        }
    }
}
