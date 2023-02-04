using System.Collections.Generic;

namespace funcInfrastructureAsCode.Functions.Models
{
    public class TerraformRoot
    {
        public Provider[] Provider { get; set; } = new[] { new Provider() };
        public List<string> Resource { get; set; } = new List<string>();

        public TerraformRoot()
        {
            Resource.Add("resourceGroup");
            Resource.Add("virtualNetwork");
            Resource.Add("subnets");
            Resource.Add("interfaces");
            Resource.Add("vistualMachine");
        }
    }

    public class Provider
    {
        public Azurerm[] Azurerm { get; set; } = new[] { new Azurerm() };
    }

    public class Azurerm
    {
        public Feature[] Features { get; set; } = new[] { new Feature() };
    }

    public class Feature
    {

    }
}