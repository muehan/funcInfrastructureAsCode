using System;
using System.Collections.Generic;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Factories;

namespace funcInfrastructureAsCode.Functions.Builder
{
    public class VirtualMachineBuilder
    {
        private readonly JsonFactory _jsonFactory;

        public VirtualMachineBuilder()
        {
            _jsonFactory = new JsonFactory();
        }

        public string Create(
            List<VirtualMachine> virtualMachines)
        {
            var resources = new List<Object>();

            foreach (var VirtualMachine in virtualMachines)
            {
                resources
                    .Add(
                        VirtualMachine
                            .TerraFormStructure);
            }

            var json = _jsonFactory
                .Create(
                    new { azurerm_linux_virtual_machine = new[] { new { example = resources } } }
                );

            return json;
        }
    }
}