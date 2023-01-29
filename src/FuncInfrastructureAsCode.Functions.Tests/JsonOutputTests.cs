using funcInfrastructureAsCode.Functions.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FuncInfrastructureAsCodeTests;

[TestFixture]
public class JsonOutputTests
{
    [Test]
    public void GenerateRootFile()
    {
        var output = new TerraformRoot();

        var serializerSettings = new JsonSerializerSettings();
        serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        string json = JsonConvert
            .SerializeObject(
                output,
                Formatting.Indented,
                serializerSettings);

        string expected = "{\r\n  \"provider\": [\r\n    {\r\n      \"azurerm\": [\r\n        {\r\n          \"features\": [\r\n            {}\r\n          ]\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}";

        Assert.That(json, Is.EqualTo(expected));
    }
}