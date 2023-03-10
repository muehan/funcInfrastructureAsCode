using System;
using System.Collections.Generic;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Factories;

namespace funcInfrastructureAsCode.Functions.Builder
{
    public class SubnetBuilder
    {
        private readonly JsonFactory _jsonFactory;

        public SubnetBuilder()
        {
            _jsonFactory = new JsonFactory();
        }

        public string Create(
            List<Subnet> subnets)
        {
            var resources = new List<Object>();

            foreach (var subnet in subnets)
            {
                resources
                    .Add(
                        subnet
                            .TerraFormStructure);
            }

            var json = _jsonFactory
                .Create(
                    new { azurerm_subnet = resources }
                );

            json = json
                .Replace("\r\n", "\r\n    ");

            // json = $"  {json}";

            return json;
        }
    }
}