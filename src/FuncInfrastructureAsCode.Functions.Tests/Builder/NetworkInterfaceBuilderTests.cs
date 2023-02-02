namespace FuncInfrastructureAsCode.Functions.Tests.Builder
{
    public class NetworkInterfaceBuilderTests
    {

        [Test]
        public void NetworkInterfaceBuilder_Create_ReturnsValidTerraformStructure()
        {
             var models = new List<NetworkInterface>();
            models
                .Add(
                    new NetworkInterface {
                        Name = "TestNetwork",
                        Location = "WestEurope",
                        ResourceGroupName = "TestResource",
                        IpConfiguratioName = "internal",
                        IpConfiguratioPrivateIpAddressAllocation = "Dynamic",
                        IpConfiguratioSubnetId = "SubnetId"
                    });

            var builder = new NetworkInterfaceBuilder();
            var actual = builder
                .Create(
                    models);

            var expect = "{\r\n  \"azurerm_network_interface\": [\r\n    {\r\n      \"example\": [\r\n        {\r\n          \"name\": \"TestNetwork\",\r\n          \"location\": \"WestEurope\",\r\n          \"resource_group_name\": \"TestResource\",\r\n          \"ip_configuration\": [\r\n            {\r\n              \"name\": \"internal\",\r\n              \"private_ip_address_allocation\": \"Dynamic\",\r\n              \"subnet_id\": \"SubnetId\"\r\n            }\r\n          ]\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}";

            Assert
                .That(
                    actual,
                    Is.EqualTo(expect));
        }
    }
}