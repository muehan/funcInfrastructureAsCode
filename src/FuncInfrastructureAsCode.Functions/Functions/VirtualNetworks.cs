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
        [FunctionName("GetVirtualNetworks")]
        public static IActionResult GetVirtualNetworks(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "VirtualNetworks")] HttpRequest req,
            [Table("VirtualNetwork", Connection = "AzureWebJobsStorage")] TableClient virtualNetworkTable,
            ILogger log)
        {
            var networks = virtualNetworkTable
                .Query<VirtualNetwork>()
                .ToList();

            var results = new List<VirtualNetworkViewModel>();

            networks.ForEach(virtualNetwork =>
            {
                results
                    .Add(
                        new VirtualNetworkViewModel
                        {
                            Name = virtualNetwork.Name,
                            LocalName = virtualNetwork.LocalName,
                            Location = virtualNetwork.Location,
                            ResourceGroupName = virtualNetwork.ResourceGroupName,
                            AddressSpace = virtualNetwork.AddressSpace,
                            Status = virtualNetwork.Status,
                        }
                    );
            });

            return new OkObjectResult(results);
        }

        [FunctionName("GetVirtualNetworksByRowkey")]
        public static IActionResult GetVirtualNetworksByRowkey(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "VirtualNetworks/{rowkey}")] HttpRequest req,
            [Table("VirtualNetwork", Connection = "AzureWebJobsStorage")] TableClient virtualNetworkTable,
            ILogger log,
            string rowkey)
        {
            var networks = virtualNetworkTable
                .Query<VirtualNetwork>()
                .Where(virtualNetwork => virtualNetwork.InfrastructureRequestId == rowkey)
                .ToList();

            var results = new List<VirtualNetworkViewModel>();

            networks.ForEach(virtualNetwork =>
            {
                results
                    .Add(
                        new VirtualNetworkViewModel
                        {
                            Name = virtualNetwork.Name,
                            LocalName = virtualNetwork.LocalName,
                            Location = virtualNetwork.Location,
                            ResourceGroupName = virtualNetwork.ResourceGroupName,
                            AddressSpace = virtualNetwork.AddressSpace,
                            Status = virtualNetwork.Status,
                        }
                    );
            });

            return new OkObjectResult(results);
        }
    }
}
