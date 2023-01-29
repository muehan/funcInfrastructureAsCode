using Newtonsoft.Json;

namespace FuncInfrastructureAsCodeTests;

[TestFixture]
public class JsonOutputTests
{
    [Test]
    public void GenerateRootFile()
    {
        var output = new GenerateTerraform();

        string json = JsonConvert
            .SerializeObject(
                new {test = "test" });

        string expected = "{ \"provider\": [ { \"azurerm\": [ { \"features\": [ {} ] } ] } ],";

        Assert.That(json, Is.EqualTo(expected));
    }
}