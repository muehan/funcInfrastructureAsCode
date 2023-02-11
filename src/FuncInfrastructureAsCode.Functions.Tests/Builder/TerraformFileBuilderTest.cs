namespace FuncInfrastructureAsCode.Functions.Tests.Builder
{
    [TestFixture]
    public class TerraformFileBuilderTest
    {
        [Test]
        public void TerraformFileBuilder_Create_ReturnsValidTerraformStructure()
        {
            var resourceGroups = new List<ResourceGroup>();
            resourceGroups
                .Add(
                    new ResourceGroup
                    {
                        Name = "TestResource",
                        LocalName = "example",
                        Location = "WestEurope",
                    });

            var virtualNetwork = new List<VirtualNetwork>();
            virtualNetwork.Add(
                new VirtualNetwork
                {
                    Name = "VirtualNetworkName",
                    LocalName = "example",
                    Location = "WestEurope",
                    ResourceGroupName = "TestResource",
                    AddressSpace = "10.0.0.0/16",
                });

            var subnets = new List<Subnet>();
            subnets.Add(new Subnet
            {
                Name = "SubnetTest",
                LocalName = "example",
                ResourceGroupName = "TestResource",
                VirtualNetworkName = "${azurerm_virtual_network.example.name}",
                AddressPrefixes = "10.0.2.0/24",
            });

            var interfaces = new List<NetworkInterface>();
            interfaces.Add(new NetworkInterface
            {
                Name = "NetworkInterfaceName",
                LocalName = "example",
                Location = "WestEurope",
                ResourceGroupName = "TestResource",
                IpConfiguratioName = "internal",
                IpConfiguratioPrivateIpAddressAllocation = "Dynamic",
                IpConfiguratioSubnetId = "${azurerm_subnet.example.id}"
            });

            var vms = new List<VirtualMachine>();
            vms.Add(new VirtualMachine
            {
                Name = "MyFancyVm",
                LocalName = "example",
                Location = "WestEurope",
                ResourceGroupName = "TestResource",
                Size = "Standard_F2",
                AdminUsername = "adminuser",
                AdminSshKeyPublicKey = "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABgQDCVz3bmxl2xz\n",
                AdminSshKeyUsername = "adminuser",
                NetworkInterfaceIds = "${azurerm_network_interface.example.id}",
                OsDiskCachine = "ReadWrite",
                OsDiskStorageAccountType = "Standard_LRS",
                SourceImageReferenceOffer = "UbuntuServer",
                SourceImageReferencePublisher = "Canonical",
                SourceImageReferenceSku = "16.04-LTS",
                SourceImageReferenceVersion = "latest",
            });

            var builder = new TerraformFileBuilder();
            var actual = builder
                .Create(
                    resourceGroups,
                    virtualNetwork,
                    subnets,
                    interfaces,
                    vms);

            var expect = File.ReadAllText("Builder\\main.ts.json");

            Assert
                .That(
                    actual.Replace(" ", ""),
                    Is.EqualTo(expect.Replace(" ", "")));
        }
    }
}