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

namespace FuncInfrastructureAsCode.Functions.Functions
{
    public static class ResourceGroups
    {
        [FunctionName("ResourceGroups")]
        public static IActionResult GetResourceGroups(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Table("RecourceGroup", Connection = "AzureWebJobsStorage")] TableClient resourceGroupTable,
            ILogger log)
        {
            var groups = resourceGroupTable
                .Query<ResourceGroup>()
                .ToList();

            var results = new List<ResourceGroupViewModel>();

            groups.ForEach(group =>
            {
                results
                    .Add(
                        new ResourceGroupViewModel
                        {
                            Name = group.Name,
                            LocalName = group.LocalName,
                            Location = group.Location,
                            Status = group.Status,
                        }
                    );
            });

            return new OkObjectResult(results);
        }

        [FunctionName("ResourceGroupByRequestId")]
        public static IActionResult ResourceGroupByRequestId(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ResourceGroups/{rowkey}")] HttpRequest req,
            [Table("RecourceGroup", Connection = "AzureWebJobsStorage")] TableClient resourceGroupTable,
            ILogger log,
            string rowkey)
        {
            var groups = resourceGroupTable
                .Query<ResourceGroup>()
                .Where(group => group.InfrastructureRequestId == rowkey)
                .ToList();

            var results = new List<ResourceGroupViewModel>();

            groups.ForEach(group =>
            {
                results
                    .Add(
                        new ResourceGroupViewModel
                        {
                            Name = group.Name,
                            LocalName = group.LocalName,
                            Location = group.Location,
                            Status = group.Status,
                        }
                    );
            });

            return new OkObjectResult(results);
        }
    }
}
