namespace FuncInfrastructureAsCode.Functions.Tests.Builder
{
    public class VirtualNetworkBuilderTests
    {

        [Test]
        public void VirtualNetworkBuilder_Create_ReturnsValidTerraformStructure()
        {
             var models = new List<VirtualNetwork>();
            models
                .Add(
                    new VirtualNetwork {
                        Name = "TestNetwork",
                        Location = "WestEurope",
                        ResourceGroupName = "TestResource",
                        AddressSpace = "10.0.0.0/16"
                    });

            var builder = new VirtualNetworkBuilder();
            var actual = builder
                .Create(
                    models);

            var expect = "{\r\n  \"azurerm_virtual_network\": [\r\n    {\r\n      \"example\": [\r\n        {\r\n          \"address_space\": [\r\n            \"10.0.0.0/16\"\r\n          ],\r\n          \"location\": \"WestEurope\",\r\n          \"name\": \"TestNetwork\",\r\n          \"resource_group_name\": \"TestResource\"\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}";

            Assert
                .That(
                    actual,
                    Is.EqualTo(expect));
        }
    }
}