namespace FuncInfrastructureAsCode.Functions.Tests.Builder
{
    [TestFixture]
    public class SubnetBuilderTests
    {
         [Test]
        public void SubnetBuilder_Create_ReturnsValidTerraformStructure()
        {
            var models = new List<Subnet>();
            models
                .Add(
                    new Subnet {
                        Name = "TestResource",
                        AddressPrefixes = "10.0.2.0/24",
                        ResourceGroupName = "DevelopmentResource",
                        VirtualNetworkName = ""
                    });

            var builder = new SubnetBuilder();
            var actual = builder
                .Create(
                    models);

            var expect = " {\r\n	\"azurerm_subnet\": [\r\n		{\r\n			\"example\": [\r\n				{\r\n					\"address_prefixes\": [\r\n						\"10.0.2.0/24\"\r\n					],\r\n					\"name\": \"dev-network-sub\",\r\n					\"resource_group_name\": \"DevelopmentResource\",\r\n					\"virtual_network_name\": \"TestNetwork\"\r\n				}\r\n			]\r\n		}\r\n	]\r\n}";

            Assert
                .That(
                    actual,
                    Is.EqualTo(expect));
        }
    }
}