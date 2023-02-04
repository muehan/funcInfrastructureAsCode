namespace funcInfrastructureAsCode.Functions.Models
{
    public class NetworkInterfaceViewModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string ResourceGroupName { get; set; }
        public string IpConfiguratioName { get; set; }
        public string IpConfiguratioPrivateIpAddressAllocation { get; set; }
        // public string IpConfiguratioSubnetId { get; set; } // set dynamic
    }
}