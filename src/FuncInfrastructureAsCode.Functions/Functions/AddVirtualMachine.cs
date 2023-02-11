using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Commands;
using System;

namespace funcInfrastructureAsCode.Functions.Functions
{
    public static class AddVirtualMachine
    {
        [FunctionName("AddVirtualMachine")]
        [return: Queue("terraformTrigger", Connection = "AzureWebJobsStorage")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] CreateVirtualMachineCommand command,
            [Table("RecourceGroup", Connection = "AzureWebJobsStorage")] IAsyncCollector<ResourceGroup> resourceGroupTable,
            [Table("Subnet", Connection = "AzureWebJobsStorage")] IAsyncCollector<Subnet> subnetTable,
            [Table("NetworkInterface", Connection = "AzureWebJobsStorage")] IAsyncCollector<NetworkInterface> netowrkInterfaceTable,
            [Table("VirtualNetwork", Connection = "AzureWebJobsStorage")] IAsyncCollector<VirtualNetwork> virtualNetworkTable,
            [Table("VirtualMachine", Connection = "AzureWebJobsStorage")] IAsyncCollector<VirtualMachine> virtualMachineTable,
            ILogger log)
        {
            log.LogInformation("AddVirtualMachine trigger");

            var resourceGroup = new ResourceGroup
            {
                RowKey = Guid.NewGuid().ToString("n"),
                Name = command.ResourceGroup.Name,
                LocalName = command.ResourceGroup.LocalName,
                Location = command.ResourceGroup.Location,
                PartitionKey = command.ResourceGroup.Name,
            };

            await resourceGroupTable
                .AddAsync(
                    resourceGroup);


            var virtuelnetwork = new VirtualNetwork
            {
                RowKey = Guid.NewGuid().ToString("n"),
                Name = command.VirtualNetwork.Name,
                LocalName = command.VirtualNetwork.LocalName,
                Location = command.VirtualNetwork.Location,
                ResourceGroupName = $"${{azurerm_resource_group.{command.ResourceGroup.LocalName}.name}}",
                AddressSpace = command.VirtualNetwork.AddressSpace,
                PartitionKey = command.ResourceGroup.Name,
            };

            await virtualNetworkTable
                .AddAsync(
                    virtuelnetwork);

            var subnet = new Subnet
            {
                RowKey = Guid.NewGuid().ToString("n"),
                Name = command.Subnet.Name,
                LocalName = command.Subnet.LocalName,
                ResourceGroupName = $"${{azurerm_resource_group.{command.ResourceGroup.LocalName}.name}}",
                AddressPrefixes = command.Subnet.AddressPrefixes,
                VirtualNetworkName = $"${{azurerm_virtual_network.{command.VirtualNetwork.LocalName}.name}}",
                PartitionKey = command.ResourceGroup.Name,
            };

            await subnetTable
                .AddAsync(
                    subnet);

            var networkInterface = new NetworkInterface
            {
                RowKey = Guid.NewGuid().ToString("n"),
                Name = command.NetworkInterface.Name,
                LocalName = command.NetworkInterface.LocalName,
                Location = command.NetworkInterface.Location,
                ResourceGroupName = $"${{azurerm_resource_group.{command.ResourceGroup.LocalName}.name}}",
                IpConfiguratioName = command.NetworkInterface.IpConfiguratioName,
                IpConfiguratioPrivateIpAddressAllocation = command.NetworkInterface.IpConfiguratioPrivateIpAddressAllocation,
                IpConfiguratioSubnetId = $"${{azurerm_subnet.{command.Subnet.LocalName}.id}}",
                PartitionKey = command.ResourceGroup.Name,
            };

            await netowrkInterfaceTable
                .AddAsync(
                    networkInterface);

            var virtualMachine = new VirtualMachine
            {
                RowKey = Guid.NewGuid().ToString("n"),
                Name = command.VirtualMachine.Name,
                LocalName = command.VirtualMachine.LocalName,
                Location = command.VirtualMachine.Location,
                ResourceGroupName = $"${{azurerm_resource_group.{command.ResourceGroup.LocalName}.name}}",
                AdminUsername = command.VirtualMachine.AdminUsername,
                Size = command.VirtualMachine.Size,
                AdminSshKeyPublicKey = command.VirtualMachine.AdminSshKeyPublicKey,
                AdminSshKeyUsername = command.VirtualMachine.AdminSshKeyUsername,
                NetworkInterfaceIds = $"${{azurerm_network_interface.{command.NetworkInterface.LocalName}.id}}",
                OsDiskCachine = command.VirtualMachine.OsDiskCachine,
                OsDiskStorageAccountType = command.VirtualMachine.OsDiskStorageAccountType,
                SourceImageReferenceOffer = command.VirtualMachine.SourceImageReferenceOffer,
                SourceImageReferencePublisher = command.VirtualMachine.SourceImageReferencePublisher,
                SourceImageReferenceSku = command.VirtualMachine.SourceImageReferenceSku,
                SourceImageReferenceVersion = command.VirtualMachine.SourceImageReferenceVersion,
                PartitionKey = command.ResourceGroup.Name,
            };

            await virtualMachineTable
                .AddAsync(
                    virtualMachine);

            return new OkObjectResult(new {
                resourceGroup = resourceGroup,
                virtuelnetwork = virtuelnetwork,
                subnet = subnet,
                networkInterface = networkInterface,
                virtualMachine = virtualMachine
            });
        }
    }
}
