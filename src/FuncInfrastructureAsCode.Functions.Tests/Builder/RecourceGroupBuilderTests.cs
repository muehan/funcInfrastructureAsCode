namespace FuncInfrastructureAsCode.Functions.Tests.Builder
{
    [TestFixture]
    public class RecourceGroupBuilderTests
    {
        [Test]
        public void GenerateRootFile()
        {
            var models = new List<ResourceGroup>();
            models
                .Add(
                    new ResourceGroup {
                        Name = "TestResource",
                        Location = "WestEurope"
                    });

            var builder = new RecourceGroupBuilder();
            var actual = builder
                .Create(
                    models);

            var expect = "{\r\n  \"azurerm_resource_group\": [\r\n    {\r\n      \"example\": [\r\n        {\r\n          \"location\": \"WestEurope\",\r\n          \"name\": \"TestResource\"\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}";

            Assert
                .That(
                    actual,
                    Is.EqualTo(expect));
        }
    }
}