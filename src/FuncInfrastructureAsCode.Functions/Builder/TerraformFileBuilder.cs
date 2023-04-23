using System.Collections.Generic;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Factories;
using funcInfrastructureAsCode.Functions.Models;
using Microsoft.Extensions.Logging;

namespace funcInfrastructureAsCode.Functions.Builder
{
    public class TerraformFileBuilder
    {
        public string Create(
            List<ResourceGroup> resourceGroups,
            List<VirtualNetwork> virtualNetworks,
            List<Subnet> subnets,
            List<NetworkInterface> networkInterfaces,
            List<VirtualMachine> virtualMachines,
            ILogger log)
        {
            var resourceBuilder = new RecourceGroupBuilder();
            var virtualNetwork = new VirtualNetworkBuilder();
            var subnetBuilder = new SubnetBuilder();
            var networkInterfaceBuilder = new NetworkInterfaceBuilder();
            var virtualMachineBuilder = new VirtualMachineBuilder();
            var root = new TerraformRoot();
            var jsonFactory = new JsonFactory();
            var json = jsonFactory.Create(root);

            log.LogInformation("created empty json file");

            json = json.Replace(
                "\"resourceGroup\"",
                resourceBuilder
                    .Create(
                        resourceGroups));

            json = json.Replace(
                "\"virtualNetwork\"",
                 virtualNetwork
                    .Create(
                        virtualNetworks));

            json = json.Replace(
                "\"subnets\"",
                 subnetBuilder
                        .Create(
                            subnets));

            json = json.Replace(
                "\"interfaces\"",
                networkInterfaceBuilder
                        .Create(
                            networkInterfaces));

            json = json.Replace(
                "\"vistualMachine\"",
                virtualMachineBuilder
                        .Create(
                            virtualMachines));

            return $"{json}\r\n";
        }
    }
}