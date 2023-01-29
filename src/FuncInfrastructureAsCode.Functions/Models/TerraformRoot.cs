namespace funcInfrastructureAsCode.Models
{
    public class GenerateTerraform
    {
        public Provider[] Provider { get; set; }
    }

    public class Provider
    {
        public Azurerm[] Azurerm { get; set; }
    }

    public class Azurerm
    {
        public Feature[] Features { get; set; } = new[] { new Feature() };
    }

    public class Feature
    {

    }
}