using System;
using System.Collections.Generic;
using funcInfrastructureAsCode.Functions.Models;
using Newtonsoft.Json;

namespace funcInfrastructureAsCode.Functions.Commands
{
    public class CreateVirtualMachineCommand
    {
        [JsonIgnore]
        public Guid? Id { get; set; } = null;
        public InfrastructureRequestCreateModel InfrastructureRequest { get; set; }
        public ResourceGroupViewModel ResourceGroup { get; set; }
        public VirtualNetworkViewModel VirtualNetwork { get; set; }
        public SubnetViewModel Subnet { get; set; }
        public NetworkInterfaceViewModel NetworkInterface { get; set; }
        public VirtualMachineViewModel VirtualMachine { get; set; }

        public CreateVirtualMachineCommand()
        {
            Id = Guid.NewGuid();
        }

        internal List<string> Validate()
        {
            var errors = new List<string>();

            if (ResourceGroup == null)
            {
                errors.Add("ResourceGroup is required");
            }

            if (VirtualNetwork == null)
            {
                errors.Add("VirtualNetwork is required");
            }

            if (Subnet == null)
            {
                errors.Add("Subnet is required");
            }

            if (NetworkInterface == null)
            {
                errors.Add("NetworkInterface is required");
            }

            if (VirtualMachine == null)
            {
                errors.Add("VirtualMachine is required");
            }

            ResourceGroup?
                .Validate(
                    errors);

            VirtualNetwork?
                .Validate(
                    errors);

            Subnet?
                .Validate(
                    errors);

            NetworkInterface?
                .Validate(
                    errors);

            VirtualMachine?
                .Validate(
                    errors);

            return errors;
        }
    }
}