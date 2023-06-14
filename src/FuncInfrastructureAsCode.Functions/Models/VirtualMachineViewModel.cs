using System.Collections.Generic;

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
        public string Status { get; set; }

        internal void Validate(List<string> errors)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                errors.Add("VirtualMachine.Name is required");
            }

            if (string.IsNullOrWhiteSpace(LocalName))
            {
                errors.Add("VirtualMachine.LocalName is required");
            }

            if (string.IsNullOrWhiteSpace(Location))
            {
                errors.Add("VirtualMachine.Location is required");
            }

            if (string.IsNullOrWhiteSpace(ResourceGroupName))
            {
                errors.Add("VirtualMachine.ResourceGroupName is required");
            }

            if (string.IsNullOrWhiteSpace(AdminUsername))
            {
                errors.Add("VirtualMachine.AdminUsername is required");
            }

            if (string.IsNullOrWhiteSpace(Size))
            {
                errors.Add("VirtualMachine.Size is required");
            }

            if (string.IsNullOrWhiteSpace(AdminSshKeyPublicKey))
            {
                errors.Add("VirtualMachine.AdminSshKeyPublicKey is required");
            }

            if (string.IsNullOrWhiteSpace(AdminSshKeyUsername))
            {
                errors.Add("VirtualMachine.AdminSshKeyUsername is required");
            }

            if (string.IsNullOrWhiteSpace(OsDiskCachine))
            {
                errors.Add("VirtualMachine.OsDiskCachine is required");
            }

            if (string.IsNullOrWhiteSpace(OsDiskStorageAccountType))
            {
                errors.Add("VirtualMachine.OsDiskStorageAccountType is required");
            }

            if (string.IsNullOrWhiteSpace(SourceImageReferenceOffer))
            {
                errors.Add("VirtualMachine.SourceImageReferenceOffer is required");
            }

            if (string.IsNullOrWhiteSpace(SourceImageReferencePublisher))
            {
                errors.Add("VirtualMachine.SourceImageReferencePublisher is required");
            }

            if (string.IsNullOrWhiteSpace(SourceImageReferenceSku))
            {
                errors.Add("VirtualMachine.SourceImageReferenceSku is required");
            }

            if (string.IsNullOrWhiteSpace(SourceImageReferenceVersion))
            {
                errors.Add("VirtualMachine.SourceImageReferenceVersion is required");
            }
        }
    }
}