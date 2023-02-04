using funcInfrastructureAsCode.Functions.Models;

namespace funcInfrastructureAsCode.Functions.Commands
{
    public class CreateVirtualMachineCommand
    {
        public ResourceGroupViewModel ResourceGroup { get; set; }
        public VirtualNetworkViewModel VirtualNetwork { get; set; }
        public SubnetViewModel Subnet { get; set; }
        public NetworkInterfaceViewModel NetworkInterface { get; set; }
        public VirtualMachineViewModel VirtualMachine { get; set; }
    }
}