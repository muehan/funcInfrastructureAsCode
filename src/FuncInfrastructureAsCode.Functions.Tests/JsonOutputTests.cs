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
                serializerSettings);

        string expected = "{\"provider\":[{\"azurerm\":[{\"features\":[{}]}]}]}";

        Assert.That(json, Is.EqualTo(expected));
    }
}