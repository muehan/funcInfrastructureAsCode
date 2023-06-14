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
    public static class VirtualMachines
    {
        [FunctionName("GetVirtualMachines")]
        public static IActionResult GetVirtualMachines(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            [Table("VirtualMachine", Connection = "AzureWebJobsStorage")] TableClient virtualNetworkTable,
            ILogger log)
        {
            var networks = virtualNetworkTable
                .Query<VirtualMachine>()
                .ToList();

            var results = new List<VirtualMachineViewModel>();

            networks.ForEach(virtualMachine =>
            {
                results
                    .Add(
                        new VirtualMachineViewModel
                        {
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
                            Status = virtualMachine.Status,
                        }
                    );
            });

            return new OkObjectResult(results);
        }

        [FunctionName("GetVirtualMachinesByRowkey")]
        public static IActionResult GetVirtualMachinesByRowkey(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "VirtualMachines/{rowkey}")] HttpRequest req,
            [Table("VirtualMachine", Connection = "AzureWebJobsStorage")] TableClient virtualNetworkTable,
            ILogger log,
            string rowkey)
        {
            var networks = virtualNetworkTable
                .Query<VirtualMachine>()
                .Where(virtualMachine => virtualMachine.InfrastructureRequestId == rowkey)
                .ToList();

            var results = new List<VirtualMachineViewModel>();

            networks.ForEach(virtualMachine =>
            {
                results
                    .Add(
                        new VirtualMachineViewModel
                        {
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
                            Status = virtualMachine.Status,
                        }
                    );
            });

            return new OkObjectResult(results);
        }
    }
}
