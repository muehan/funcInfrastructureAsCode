using System;
using System.Collections.Generic;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Factories;

namespace funcInfrastructureAsCode.Functions.Builder
{
    public class VirtualNetworkBuilder
    {
        private readonly JsonFactory _jsonFactory;

        public VirtualNetworkBuilder()
        {
            _jsonFactory = new JsonFactory();
        }

        public string Create(
            List<VirtualNetwork> networks)
        {
            var resources = new List<Object>();

            foreach (var network in networks)
            {
                resources
                    .Add(
                        network
                            .TerraFormStructure);
            }

            var json = _jsonFactory
                .Create(
                    new { azurerm_virtual_network = resources }
                );

            json = json
                .Replace("\r\n", "\r\n    ");

            // json = $"  {json}";

            return json;
        }
    }
}