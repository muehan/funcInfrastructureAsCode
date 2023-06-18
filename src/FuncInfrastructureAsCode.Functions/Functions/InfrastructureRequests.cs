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

        [FunctionName("InfrastructureRequestsSetStatusToActive")]
        public static IActionResult SetStatusToActive(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "InfrastructureRequests/{id}")] HttpRequest req,
            [Table("InfrastructureRequest", Connection = "AzureWebJobsStorage")] TableClient requestTable,
            [Table("RecourceGroup", Connection = "AzureWebJobsStorage")] TableClient resourceGroupTable,
            [Table("NetworkInterface", Connection = "AzureWebJobsStorage")] TableClient networkInterfaceTable,
            [Table("Subnet", Connection = "AzureWebJobsStorage")] TableClient subnetTable,
            [Table("VirtualNetwork", Connection = "AzureWebJobsStorage")] TableClient virtualNetworkTable,
            [Table("VirtualMachine", Connection = "AzureWebJobsStorage")] TableClient virtualMachineTable,
            ILogger log,
            string id)
        {
            var entitiy = requestTable
                .Query<InfrastructureRequest>()
                .Where(x => x.RowKey == id)
                .FirstOrDefault();

            entitiy.RequestStatus = "Active";

            requestTable
                .UpdateEntity<InfrastructureRequest>(
                    entitiy,
                    entitiy.ETag,
                    TableUpdateMode.Replace);

            var resourceGroup = resourceGroupTable
                .Query<ResourceGroup>()
                .Where(x => x.InfrastructureRequestId == id)
                .FirstOrDefault();

            resourceGroup.Status = "Active";

            resourceGroupTable
                .UpdateEntity<ResourceGroup>(
                    resourceGroup,
                    resourceGroup.ETag,
                    TableUpdateMode.Replace);

            var networkInterfaces = networkInterfaceTable
                .Query<NetworkInterface>()
                .Where(x => x.InfrastructureRequestId == id)
                .ToList();

            networkInterfaces.ForEach(networkInterface =>
            {
                networkInterface.Status = "Active";

                networkInterfaceTable
                    .UpdateEntity<NetworkInterface>(
                        networkInterface,
                        networkInterface.ETag,
                        TableUpdateMode.Replace);
            });

            var subnets = subnetTable
                .Query<Subnet>()
                .Where(x => x.InfrastructureRequestId == id)
                .ToList();

            subnets.ForEach(subnet =>
            {
                subnet.Status = "Active";

                subnetTable
                    .UpdateEntity<Subnet>(
                        subnet,
                        subnet.ETag,
                        TableUpdateMode.Replace);
            });

            var virtualNetworks = virtualNetworkTable
                .Query<VirtualNetwork>()
                .Where(x => x.InfrastructureRequestId == id)
                .ToList();

            virtualNetworks.ForEach(virtualNetwork =>
            {
                virtualNetwork.Status = "Active";

                virtualNetworkTable
                    .UpdateEntity<VirtualNetwork>(
                        virtualNetwork,
                        virtualNetwork.ETag,
                        TableUpdateMode.Replace);
            });

            var virtualMachines = virtualMachineTable
                .Query<VirtualMachine>()
                .Where(x => x.InfrastructureRequestId == id)
                .ToList();

            virtualMachines.ForEach(virtualMachine =>
            {
                virtualMachine.Status = "Active";

                virtualMachineTable
                    .UpdateEntity<VirtualMachine>(
                        virtualMachine,
                        virtualMachine.ETag,
                        TableUpdateMode.Replace);
            });

            return new OkObjectResult(new InfrastructureRequestViewModel
            {
                RowKey = entitiy.RowKey,
                RequesterName = entitiy.RequesterName,
                RequesterEmail = entitiy.RequesterEmail,
                RequestStatus = entitiy.RequestStatus,
                CreatedAt = entitiy.CreatedAt
            });
        }
    }
}
