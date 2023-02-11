namespace funcInfrastructureAsCode.Functions.Models
{
    public class VirtualNetworkViewModel
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string Location { get; set; }
        public string ResourceGroupName { get; set; }
        public string AddressSpace { get; set; }
    }
}