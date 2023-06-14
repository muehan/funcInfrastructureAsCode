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
        [FunctionName("GetSubnets")]
        public static IActionResult GetSubnets(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Table("Subnet", Connection = "AzureWebJobsStorage")] TableClient subnetTable,
            ILogger log)
        {
            var subnets = subnetTable
                .Query<Subnet>()
                .ToList();

            var results = new List<SubnetViewModel>();

            subnets
                .ForEach(subnet =>
                {
                    results
                        .Add(
                            new SubnetViewModel
                            {
                                Name = subnet.Name,
                                LocalName = subnet.LocalName,
                                ResourceGroupName = subnet.ResourceGroupName,
                                AddressPrefixes = subnet.AddressPrefixes,
                                VirtualNetworkName = subnet.VirtualNetworkName,
                                Status = subnet.Status,
                            }
                        );
                });

            return new OkObjectResult(results);
        }

        [FunctionName("GetSubnetsByRowkey")]
        public static IActionResult GetSubnetsByRowkey(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Subnet/{rowkey}")] HttpRequest req,
            [Table("Subnet", Connection = "AzureWebJobsStorage")] TableClient subnetTable,
            ILogger log,
            string rowkey)
        {
            var subnets = subnetTable
                .Query<Subnet>()
                .Where(subnet => subnet.InfrastructureRequestId == rowkey)
                .ToList();

            var results = new List<SubnetViewModel>();

            subnets
                .ForEach(subnet =>
                {
                    results
                        .Add(
                            new SubnetViewModel
                            {
                                Name = subnet.Name,
                                LocalName = subnet.LocalName,
                                ResourceGroupName = subnet.ResourceGroupName,
                                AddressPrefixes = subnet.AddressPrefixes,
                                VirtualNetworkName = subnet.VirtualNetworkName,
                                Status = subnet.Status,
                            }
                        );
                });

            return new OkObjectResult(results);
        }
    }
}
