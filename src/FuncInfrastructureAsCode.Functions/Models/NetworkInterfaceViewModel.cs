using System;
using System.Collections.Generic;

namespace funcInfrastructureAsCode.Functions.Models
{
    public class NetworkInterfaceViewModel
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string Location { get; set; }
        public string ResourceGroupName { get; set; }
        public string IpConfiguratioName { get; set; }
        public string IpConfiguratioPrivateIpAddressAllocation { get; set; }

        internal void Validate(List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                errors.Add("NetworkInterface.Name is required");
            }

            if (string.IsNullOrWhiteSpace(LocalName))
            {
                errors.Add("NetworkInterface.LocalName is required");
            }

            if (string.IsNullOrWhiteSpace(Location))
            {
                errors.Add("NetworkInterface.Location is required");
            }

            if (string.IsNullOrWhiteSpace(ResourceGroupName))
            {
                errors.Add("NetworkInterface.ResourceGroupName is required");
            }

            if (string.IsNullOrWhiteSpace(IpConfiguratioName))
            {
                errors.Add("NetworkInterface.IpConfiguratioName is required");
            }

            if (string.IsNullOrWhiteSpace(IpConfiguratioPrivateIpAddressAllocation))
            {
                errors.Add("NetworkInterface.IpConfiguratioPrivateIpAddressAllocation is required");
            }

        }
        // public string IpConfiguratioSubnetId { get; set; } // set dynamic
    }
}