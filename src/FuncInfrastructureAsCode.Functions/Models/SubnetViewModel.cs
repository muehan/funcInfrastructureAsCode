namespace funcInfrastructureAsCode.Functions.Models
{
    public class SubnetViewModel
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string ResourceGroupName { get; set; }
        public string AddressPrefixes { get; set; }
        public string VirtualNetworkName { get; set; }
    }
}