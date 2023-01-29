namespace funcInfrastructureAsCode.Functions.Models
{
    public class TerraformRoot
    {
        public Provider[] Provider { get; set; } = new [] { new Provider() };
    }

    public class Provider
    {
        public Azurerm[] Azurerm { get; set; } = new [] { new Azurerm() };
    }

    public class Azurerm
    {
        public Feature[] Features { get; set; } = new[] { new Feature() };
    }

    public class Feature
    {
        
    }
}