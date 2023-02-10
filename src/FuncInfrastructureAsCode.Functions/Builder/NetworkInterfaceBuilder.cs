using System;
using System.Collections.Generic;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Factories;

namespace funcInfrastructureAsCode.Functions.Builder
{
    public class NetworkInterfaceBuilder
    {
        private readonly JsonFactory _jsonFactory;

        public NetworkInterfaceBuilder()
        {
            _jsonFactory = new JsonFactory();
        }

        public string Create(
            List<NetworkInterface> interfaces)
        {
            var resources = new List<Object>();

            foreach (var networkInterface in interfaces)
            {
                resources
                    .Add(
                        networkInterface
                            .TerraFormStructure);
            }

            var json = _jsonFactory
                .Create(
                    new { azurerm_network_interface = new[] { new { example = resources } } }
                );

            json = json
                .Replace("\r\n", "\r\n    ");

            // json = $"  {json}";

            return json;
        }
    }
}