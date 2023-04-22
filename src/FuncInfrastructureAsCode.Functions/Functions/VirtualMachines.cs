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
    public static class VirtualMachines
    {
        [FunctionName("VirtualMachines")]
       public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Table("VirtuaMachines", Connection = "AzureWebJobsStorage")] TableClient virtualNetworkTable,
            ILogger log)
        {
            var subnets = virtualNetworkTable.Query<VirtualMachine>().ToList();

            var results = new List<VirtualMachineViewModel>();

            subnets.ForEach(virtualMachine =>
            {
                results
                    .Add(
                        new VirtualMachineViewModel {
                            Name = virtualMachine.Name,
                            LocalName = virtualMachine.LocalName,
                            Location = virtualMachine.Location,
                            ResourceGroupName = virtualMachine.ResourceGroupName,
                            AdminUsername = virtualMachine.AdminUsername,
                            Size = virtualMachine.Size,
                            OsDiskCachine = virtualMachine.OsDiskCachine,
                            OsDiskStorageAccountType = virtualMachine.OsDiskStorageAccountType,
                            SourceImageReferenceOffer = virtualMachine.SourceImageReferenceOffer,
                            SourceImageReferencePublisher = virtualMachine.SourceImageReferencePublisher,
                            SourceImageReferenceSku = virtualMachine.SourceImageReferenceSku,
                            SourceImageReferenceVersion = virtualMachine.SourceImageReferenceVersion,
                        }
                    );
            });

            return new OkObjectResult(results);
        }
    }
}
