using System;
using System.Collections.Generic;

namespace funcInfrastructureAsCode.Functions.Models
{
    public class SubnetViewModel
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string ResourceGroupName { get; set; }
        public string AddressPrefixes { get; set; }
        public string VirtualNetworkName { get; set; }

        internal void Validate(
            List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                errors.Add("Subnet.Name is required");
            }

            if (string.IsNullOrWhiteSpace(LocalName))
            {
                errors.Add("Subnet.LocalName is required");
            }

            if (string.IsNullOrWhiteSpace(ResourceGroupName))
            {
                errors.Add("Subnet.ResourceGroupName is required");
            }

            if (string.IsNullOrWhiteSpace(AddressPrefixes))
            {
                errors.Add("Subnet.AddressPrefixes is required");
            }

            if (string.IsNullOrWhiteSpace(VirtualNetworkName))
            {
                errors.Add("Subnet.VirtualNetworkName is required");
            }
        }
    }
}