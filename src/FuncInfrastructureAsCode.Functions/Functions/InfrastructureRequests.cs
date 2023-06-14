using System.Threading.Tasks;
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
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Table("InfrastructureRequest", Connection = "AzureWebJobsStorage")] TableClient subnetTable,
            ILogger log)
        {
            var requests = subnetTable
                .Query<InfrastructureRequest>()
                .ToList();

            var results = new List<InfrastructureRequestViewModel>();

            requests.ForEach(request =>
            {
                results
                    .Add(
                        new InfrastructureRequestViewModel
                        {
                            Id = request.Id,
                            RequesterName = request.RequesterName,
                            RequesterEmail = request.RequesterEmail,
                            RequestStatus = request.RequestStatus,
                            CreatedAt = request.CreatedAt
                        }
                    );
            });

            return new OkObjectResult(results);
        }
    }
}
