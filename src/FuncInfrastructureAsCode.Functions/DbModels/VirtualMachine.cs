using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Azure;
using Azure.Data.Tables;
using funcInfrastructureAsCode.Functions.Abstraction;
using funcInfrastructureAsCode.Functions.Commands;

namespace funcInfrastructureAsCode.Functions.DbModels
{
    public class VirtualMachine
        : ITableEntity,
          IMappedEntity<VirtualMachine>
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string Location { get; set; }
        public string ResourceGroupName { get; set; }
        public string AdminUsername { get; set; }
        public string Size { get; set; }
        public string AdminSshKeyPublicKey { get; set; }
        public string AdminSshKeyUsername { get; set; }
        public string NetworkInterfaceIds { get; set; }
        public string OsDiskCachine { get; set; }
        public string OsDiskStorageAccountType { get; set; }
        public string SourceImageReferenceOffer { get; set; }
        public string SourceImageReferencePublisher { get; set; }
        public string SourceImageReferenceSku { get; set; }
        public string SourceImageReferenceVersion { get; set; }
        public string InfrastructureRequestId { get; set; }
        public string Status { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        /*
        {
            "name": "MyFancyVm",
            "location": "West Europe",
            "resource_group_name": "DevelopmentResource",
            "admin_username": "adminuser",
            "size": "Standard_F2",
            "admin_ssh_key": [
                {
                    "public_key": "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABgQDCVz3bmxl2xzgkOtX7o/6qAQ2iABJdFFPNNVk2SZ1Gc+DWwo1iCuoFjDUWfHVNDYkISEqPgC1nCKCfd4y/dBs1JNzlR37ycjJGU2pEXmQaLxSAHNnAT1Jz3oZ5+MT9hdkwleD6Q1t5QL7PoC6f0k9ZL2xi/f7aWALbVuPCdrjPAe9jgouEYaHqf/V5B8xjj5Nm7FlEiIvbME/OGWFxMXIbZJN8AIU+bjUdVRyihYj8U19IqweQDQCoK3zxeciHwkNfg599UEfi0MiqvWfukjrmx0p4bJx/72c6NgYb3lamcX+t4PwW3RJ+A0FNzDgWKKi3tPE2A1W4qwawij3Fp8Py9i+6CwSMAE/dyLHLKvNPCwIukWiAToCmHe6FDbniezjj75ME5z7yQgLVHt998HcDPVzs987vGgcszCfWiGRgFgHwuYDDTFHiaSz+t5Lmlm8xUN0rdrWxySGae4Iz71Nw0PhsEDcs82styfuy9cQhAk/t1Mkm4ITj4jFseowNpkk= tdc@workstation\n",
                    "username": "adminuser"
                }
            ],
            "network_interface_ids": [
                "${azurerm_network_interface.example.id}"
            ],
            "os_disk": [
                {
                    "caching": "ReadWrite",
                    "storage_account_type": "Standard_LRS"
                }
            ],
            "source_image_reference": [
                {
                    "offer": "UbuntuServer",
                    "publisher": "Canonical",
                    "sku": "16.04-LTS",
                    "version": "latest"
                }
            ]
        }
        */

        [IgnoreDataMember]
        public dynamic TerraFormStructure
        {
            get
            {
                var dict = new Dictionary<string, object>();

                dict
                    .Add(
                        LocalName,
                        new[] {
                            new
                            {
                                name = Name,
                                location = Location,
                                resource_group_name = ResourceGroupName,
                                admin_username = AdminUsername,
                                size = Size,
                                admin_ssh_key = new[] {
                                    new {
                                        public_key = AdminSshKeyPublicKey,
                                        username = AdminSshKeyUsername
                                    }
                                },
                                network_interface_ids = new[] { NetworkInterfaceIds },
                                os_disk = new[] {
                                    new {
                                        caching = OsDiskCachine,
                                        storage_account_type = OsDiskStorageAccountType
                                    }
                                },
                                source_image_reference = new[] {
                                    new {
                                        offer = SourceImageReferenceOffer,
                                        publisher = SourceImageReferencePublisher,
                                        sku = SourceImageReferenceSku,
                                        version = SourceImageReferenceVersion
                                    }
                                }
                            }
                            }
                        );

                return dict;
            }
        }

        public void Map(
            CreateVirtualMachineCommand command)
        {
            RowKey = Guid.NewGuid().ToString("n");
            Name = command.VirtualMachine.Name;
            LocalName = command.VirtualMachine.LocalName;
            Location = command.VirtualMachine.Location;
            ResourceGroupName = $"${{azurerm_resource_group.{command.ResourceGroup.LocalName}.name}}";
            AdminUsername = command.VirtualMachine.AdminUsername;
            Size = command.VirtualMachine.Size;
            AdminSshKeyPublicKey = command.VirtualMachine.AdminSshKeyPublicKey;
            AdminSshKeyUsername = command.VirtualMachine.AdminSshKeyUsername;
            NetworkInterfaceIds = $"${{azurerm_network_interface.{command.NetworkInterface.LocalName}.id}}";
            OsDiskCachine = command.VirtualMachine.OsDiskCachine;
            OsDiskStorageAccountType = command.VirtualMachine.OsDiskStorageAccountType;
            SourceImageReferenceOffer = command.VirtualMachine.SourceImageReferenceOffer;
            SourceImageReferencePublisher = command.VirtualMachine.SourceImageReferencePublisher;
            SourceImageReferenceSku = command.VirtualMachine.SourceImageReferenceSku;
            SourceImageReferenceVersion = command.VirtualMachine.SourceImageReferenceVersion;
            PartitionKey = command.ResourceGroup.Name;
            InfrastructureRequestId = $"{command.Id.Value}";
            Status = "Pending";
        }
    }
}