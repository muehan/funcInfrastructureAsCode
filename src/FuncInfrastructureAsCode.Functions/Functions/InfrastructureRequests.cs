using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using funcInfrastructureAsCode.Functions.DbModels;
using System.Collections.Generic;
using funcInfrastructureAsCode.Functions.Models;
using System.Linq;

namespace FuncInfrastructureAsCode.Functions.Functions
{
    public static class InfrastructureRequests
    {
        [FunctionName("InfrastructureRequests")]
        public static IActionResult Get(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Table("InfrastructureRequest", Connection = "AzureWebJobsStorage")] TableClient requestTable,
            ILogger log)
        {
            var requests = requestTable
                .Query<InfrastructureRequest>()
                .ToList();

            var results = new List<InfrastructureRequestViewModel>();

            requests.ForEach(request =>
            {
                results
                    .Add(
                        new InfrastructureRequestViewModel
                        {
                            RowKey = request.RowKey,
                            RequesterName = request.RequesterName,
                            RequesterEmail = request.RequesterEmail,
                            RequestStatus = request.RequestStatus,
                            CreatedAt = request.CreatedAt
                        }
                    );
            });

            return new OkObjectResult(results);
        }


        [FunctionName("InfrastructureRequestsById")]
        public static IActionResult GetById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "InfrastructureRequests/{partitionkey}/{id}")] HttpRequest req,
            [Table("InfrastructureRequest", "{partitionkey}", "{id}", Connection = "AzureWebJobsStorage")] InfrastructureRequest requestEntity,
            ILogger log,
            string id)
        {
            return new OkObjectResult(new InfrastructureRequestViewModel
            {
                RowKey = requestEntity.RowKey,
                RequesterName = requestEntity.RequesterName,
                RequesterEmail = requestEntity.RequesterEmail,
                RequestStatus = requestEntity.RequestStatus,
                CreatedAt = requestEntity.CreatedAt
            });
        }
    }
}
