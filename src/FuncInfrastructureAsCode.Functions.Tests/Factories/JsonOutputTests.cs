namespace FuncInfrastructureAsCodeTests.Factories;

[TestFixture]
public class JsonOutputTests
{
    [Test]
    public void GenerateRootFile()
    {
        var structure = new {
            name = "test"
        };
        var jsonFactory = new JsonFactory();

        var actual = jsonFactory
            .Create(
                structure);

        string expected = "{\r\n  \"name\": \"test\"\r\n}";

        Assert.That(actual, Is.EqualTo(expected));
    }
}