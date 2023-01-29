namespace FuncInfrastructureAsCodeTests;

[TestFixture]
public class JsonOutputTests
{
    [Test]
    public void GenerateRootFile()
    {
        var structure = new TerraformRoot();
        var jsonFactory = new JsonFactory();

        var actual = jsonFactory
            .Create(
                structure);

        string expected = "{\r\n  \"provider\": [\r\n    {\r\n      \"azurerm\": [\r\n        {\r\n          \"features\": [\r\n            {}\r\n          ]\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}";

        Assert.That(actual, Is.EqualTo(expected));
    }
}