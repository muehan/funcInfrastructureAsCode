using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Commands;
using Azure.Data.Tables;
using funcInfrastructureAsCode.Functions.Services;
using System.Linq;

namespace funcInfrastructureAsCode.Functions.Functions
{
    public static class AddVirtualMachine
    {
        [FunctionName("AddVirtualMachine")]
        // [return: Queue("terraformTrigger", Connection = "AzureWebJobsStorage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] CreateVirtualMachineCommand command,
            [Table("InfrastructureRequest", Connection = "AzureWebJobsStorage")] IAsyncCollector<InfrastructureRequest> InfrastructureRequest,
            [Table("RecourceGroup", Connection = "AzureWebJobsStorage")] IAsyncCollector<ResourceGroup> resourceGroupTable,
            [Table("RecourceGroup", Connection = "AzureWebJobsStorage")] TableClient resourceGroupQuery,
            [Table("Subnet", Connection = "AzureWebJobsStorage")] IAsyncCollector<Subnet> subnetTable,
            [Table("Subnet", Connection = "AzureWebJobsStorage")] TableClient subnetQuery,
            [Table("NetworkInterface", Connection = "AzureWebJobsStorage")] IAsyncCollector<NetworkInterface> networkInterfaceTable,
            [Table("NetworkInterface", Connection = "AzureWebJobsStorage")] TableClient networkInterfaceQuery,
            [Table("VirtualNetwork", Connection = "AzureWebJobsStorage")] IAsyncCollector<VirtualNetwork> virtualNetworkTable,
            [Table("VirtualNetwork", Connection = "AzureWebJobsStorage")] TableClient virtualNetworkQuery,
            [Table("VirtualMachine", Connection = "AzureWebJobsStorage")] IAsyncCollector<VirtualMachine> virtualMachineTable,
            [Table("VirtualMachine", Connection = "AzureWebJobsStorage")] TableClient virtualMachineQuery,
            [Queue("terraformTrigger", Connection = "AzureWebJobsStorage")] IAsyncCollector<string> terraformTriggerQueue,
            ILogger log)
        {
            log.LogInformation("AddVirtualMachine trigger");

            var errors = command.Validate();

            if (errors.Any())
            {
                return new BadRequestObjectResult(
                    String.Join(
                        ", ",
                        errors)
                );
            }

            await InfrastructureRequest
                .AddAsync(
                    new InfrastructureRequest
                    {
                        RequesterName = command.InfrastructureRequest.RequesterName,
                        RequesterEmail = command.InfrastructureRequest.RequesterEmail,
                        RequestStatus = "Pending",
                        CreatedAt = DateTime.UtcNow,
                        RowKey = $"{command.Id.Value}",
                        PartitionKey = command.InfrastructureRequest.RequesterName,
                    });

            var resourcePersistanceService = new PersistanceService(log);

            var resourceGroup = await resourcePersistanceService
                .CreateIfNotExist<ResourceGroup>(
                    resourceGroupTable,
                    resourceGroupQuery,
                    command,
                    (e) => e.Name == command.ResourceGroup.Name);


            var virtualnetwork = await resourcePersistanceService
                .CreateIfNotExist<VirtualNetwork>(
                    virtualNetworkTable,
                    virtualNetworkQuery,
                    command,
                    (e) => e.Name == command.VirtualNetwork.Name);

            var subnet = await resourcePersistanceService
                .CreateIfNotExist<Subnet>(
                    subnetTable,
                    subnetQuery,
                    command,
                    (e) => e.Name == command.Subnet.Name);

            var networkInterface = await resourcePersistanceService
                .CreateIfNotExist<NetworkInterface>(
                    networkInterfaceTable,
                    networkInterfaceQuery,
                    command,
                    (e) => e.Name == command.NetworkInterface.Name);

            var virtualMachine = await resourcePersistanceService
                .CreateIfNotExist<VirtualMachine>(
                    virtualMachineTable,
                    virtualMachineQuery,
                    command,
                    (e) => e.Name == command.VirtualMachine.Name);

            await terraformTriggerQueue
                .AddAsync(
                    $"{command.Id.Value}");

            return new OkObjectResult(new
            {
                resourceGroup = resourceGroup,
                virtuelnetwork = virtualnetwork,
                subnet = subnet,
                networkInterface = networkInterface,
                virtualMachine = virtualMachine
            });
        }
    }
}
