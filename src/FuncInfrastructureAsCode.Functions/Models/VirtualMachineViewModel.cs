namespace funcInfrastructureAsCode.Functions.Models
{
    public class VirtualMachineViewModel
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string Location { get; set; }
        public string ResourceGroupName { get; set; }
        public string AdminUsername { get; set; }
        public string Size { get; set; }
        public string AdminSshKeyPublicKey { get; set; }
        public string AdminSshKeyUsername { get; set; }
        // public string NetworkInterfaceIds { get; set; } // set dynamic
        public string OsDiskCachine { get; set; }
        public string OsDiskStorageAccountType { get; set; }
        public string SourceImageReferenceOffer { get; set; }
        public string SourceImageReferencePublisher { get; set; }
        public string SourceImageReferenceSku { get; set; }
        public string SourceImageReferenceVersion { get; set; }
    }
}