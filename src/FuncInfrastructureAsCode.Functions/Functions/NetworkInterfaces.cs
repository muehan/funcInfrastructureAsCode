using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using funcInfrastructureAsCode.Functions.DbModels;
using System.Linq;
using System.Collections.Generic;
using funcInfrastructureAsCode.Functions.Models;

namespace FuncInfrastructureAsCode.Functions
{
    public static class NetworkInterfaces
    {
        [FunctionName("NetworkInterfaces")]
                public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Table("RecourceGroup", Connection = "AzureWebJobsStorage")] TableClient resourceGroupTable,
            ILogger log)
        {
            var groups = resourceGroupTable.Query<NetworkInterface>().ToList();

            var results = new List<NetworkInterfaceViewModel>();

            groups.ForEach(network =>
            {
                results
                    .Add(
                        new NetworkInterfaceViewModel {
                            Name = network.Name,
                            LocalName = network.LocalName,
                            Location = network.Location,
                            ResourceGroupName = network.ResourceGroupName,
                            IpConfiguratioName = network.IpConfiguratioName,
                            IpConfiguratioPrivateIpAddressAllocation = network.IpConfiguratioPrivateIpAddressAllocation
                        }
                    );
            });

            return new OkObjectResult(results);
        }
    }
}
