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

namespace FuncInfrastructureAsCode.Functions.Functions
{
    public static class Subnets
    {
        [FunctionName("Subnets")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Table("Subnet", Connection = "AzureWebJobsStorage")] TableClient subnetTable,
            ILogger log)
        {
            var subnets = subnetTable.Query<Subnet>().ToList();

            var results = new List<SubnetViewModel>();

            subnets.ForEach(subnet =>
            {
                results
                    .Add(
                        new SubnetViewModel {
                            Name = subnet.Name,
                            LocalName = subnet.LocalName,
                            ResourceGroupName = subnet.ResourceGroupName,
                            AddressPrefixes = subnet.AddressPrefixes,
                            VirtualNetworkName = subnet.VirtualNetworkName
                        }
                    );
            });

            return new OkObjectResult(results);
        }
    }
}
