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
    public static class VirtualNetworks
    {
        [FunctionName("VirtualNetworks")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Table("VirtualNetwork", Connection = "AzureWebJobsStorage")] TableClient virtualNetworkTable,
            ILogger log)
        {
            var subnets = virtualNetworkTable.Query<VirtualNetwork>().ToList();

            var results = new List<VirtualNetworkViewModel>();

            subnets.ForEach(virtualNetwork =>
            {
                results
                    .Add(
                        new VirtualNetworkViewModel {
                            Name = virtualNetwork.Name,
                            LocalName = virtualNetwork.LocalName,
                            Location = virtualNetwork.Location,
                            ResourceGroupName = virtualNetwork.ResourceGroupName,
                            AddressSpace = virtualNetwork.AddressSpace
                        }
                    );
            });

            return new OkObjectResult(results);
        }
    }
}
