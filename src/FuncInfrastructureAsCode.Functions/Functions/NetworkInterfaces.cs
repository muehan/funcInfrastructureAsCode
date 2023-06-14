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

namespace FuncInfrastructureAsCode.Functions.Functions
{
    public static class NetworkInterfaces
    {
        [FunctionName("NetworkInterfaces")]
        public static IActionResult GetNetworkInterfaces(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Table("NetworkInterface", Connection = "AzureWebJobsStorage")] TableClient networkInterfaceTable,
            ILogger log)
        {
            var groups = networkInterfaceTable
                .Query<NetworkInterface>()
                .ToList();

            var results = new List<NetworkInterfaceViewModel>();

            groups.ForEach(network =>
            {
                results
                    .Add(
                        new NetworkInterfaceViewModel
                        {
                            Name = network.Name,
                            LocalName = network.LocalName,
                            Location = network.Location,
                            ResourceGroupName = network.ResourceGroupName,
                            IpConfiguratioName = network.IpConfiguratioName,
                            IpConfiguratioPrivateIpAddressAllocation = network.IpConfiguratioPrivateIpAddressAllocation,
                            Status = network.Status,
                        }
                    );
            });

            return new OkObjectResult(results);
        }

        [FunctionName("NetworkInterfacesByRequestId")]
        public static IActionResult NetworkInterfacesByRequestId(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "NetworkInterfaces({rowkey})")] HttpRequest req,
            [Table("NetworkInterface", Connection = "AzureWebJobsStorage")] TableClient networkInterfaceTable,
            ILogger log,
            string rowkey)
        {
            var groups = networkInterfaceTable
                .Query<NetworkInterface>()
                .Where(network => network.InfrastructureRequestId == rowkey)
                .ToList();

            var results = new List<NetworkInterfaceViewModel>();

            groups.ForEach(network =>
            {
                results
                    .Add(
                        new NetworkInterfaceViewModel
                        {
                            Name = network.Name,
                            LocalName = network.LocalName,
                            Location = network.Location,
                            ResourceGroupName = network.ResourceGroupName,
                            IpConfiguratioName = network.IpConfiguratioName,
                            IpConfiguratioPrivateIpAddressAllocation = network.IpConfiguratioPrivateIpAddressAllocation,
                            Status = network.Status,
                        }
                    );
            });

            return new OkObjectResult(results);
        }
    }
}
