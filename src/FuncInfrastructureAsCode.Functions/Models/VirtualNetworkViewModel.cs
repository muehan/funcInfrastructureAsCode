using System;
using System.Collections.Generic;

namespace funcInfrastructureAsCode.Functions.Models
{
    public class VirtualNetworkViewModel
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string Location { get; set; }
        public string ResourceGroupName { get; set; }
        public string AddressSpace { get; set; }

        internal void Validate(
            List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                errors.Add("VirtualNetwork.Name is required");
            }

            if (string.IsNullOrWhiteSpace(LocalName))
            {
                errors.Add("VirtualNetwork.LocalName is required");
            }

            if (string.IsNullOrWhiteSpace(Location))
            {
                errors.Add("VirtualNetwork.Location is required");
            }

            if (string.IsNullOrWhiteSpace(ResourceGroupName))
            {
                errors.Add("VirtualNetwork.ResourceGroupName is required");
            }

            if (string.IsNullOrWhiteSpace(AddressSpace))
            {
                errors.Add("VirtualNetwork.AddressSpace is required");
            }
        }
    }
}