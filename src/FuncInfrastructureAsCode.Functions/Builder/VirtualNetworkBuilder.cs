using System;
using System.Collections.Generic;
using funcInfrastructureAsCode.Functions.Factories;
using muehan.infrastructorcreater.DbModels;

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
            List<Object> resources = new List<Object>();

            foreach (var network in networks)
            {
                resources
                    .Add(
                        network
                            .TerraFormStructure);
            }

            var json = _jsonFactory
                .Create(
                    new { azurerm_virtual_network = new[] { new { example = resources } } }
                );

            return json;
        }

    }
}