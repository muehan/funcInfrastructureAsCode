using System.Collections.Generic;
using funcInfrastructureAsCode.Functions.Factories;
using System;
using funcInfrastructureAsCode.Functions.DbModels;

namespace funcInfrastructureAsCode.Functions.Builder
{
    public class RecourceGroupBuilder
    {
        private readonly JsonFactory _jsonFactory;

        public RecourceGroupBuilder()
        {
            _jsonFactory = new JsonFactory();
        }

        public string Create(
            List<ResourceGroup> resourceGroups)
        {
            var resources = new List<Object>();

            foreach (var resource in resourceGroups)
            {
                resources
                    .Add(
                        resource
                            .TerraFormStructure);
            }

            var json = _jsonFactory
                .Create(
                    new { azurerm_resource_group = resources }
                );

            json = json
                .Replace("\r\n","\r\n    ");

            // json = $"  {json}";

            return json;
        }
    }
}