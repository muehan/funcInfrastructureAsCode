namespace FuncInfrastructureAsCode.Functions.Tests.Builder
{
    public class VirtualmachineBuilderTests
    {

        [Test]
        public void VirtualMachineBuilder_Create_ReturnsValidTerraformStructure()
        {
             var models = new List<VirtualMachine>();
            models
                .Add(
                    new VirtualMachine {
                        Name = "MyFancyVm",
                        Location = "WestEurope",
                        ResourceGroupName = "TestResource",
                        AdminUsername = "adminuser",
                        Size = "Standard_F2",
                        AdminSshKeyPublicKey = "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAAAgQDArSXQCqoc3aPBDiV2FkY2Sw+DkfVAJbxdE3kDIWShwW6AGQP+Lx420yltx0EzeoLKj49IQcZteTUzy05icrYNtfJB5ulWuf41e1WW99QjoP5zZIlgl1AGKhkiOsQCwjna5ykzegMH2nbn93OT02Xdr117J29/Ef0ndUPOIEuyuw== noname",
                        AdminSshKeyUsername = "adminuser",
                        NetworkInterfaceIds = "${azurerm_network_interface.example.id}",
                        OsDiskCachine = "ReadWrite",
                        OsDiskStorageAccountType = "Standard_LRS",
                        SourceImageReferenceOffer = "UbuntuServer",
                        SourceImageReferencePublisher = "Canonical",
                        SourceImageReferenceSku = "16.04-LTS",
                        SourceImageReferenceVersion = "latest"
                    });

            var builder = new VirtualMachineBuilder();
            var actual = builder
                .Create(
                    models);

            var expect = "{\r\n      \"azurerm_linux_virtual_machine\": [\r\n        {\r\n          \"example\": [\r\n            {\r\n              \"name\": \"MyFancyVm\",\r\n              \"location\": \"WestEurope\",\r\n              \"resource_group_name\": \"TestResource\",\r\n              \"admin_username\": \"adminuser\",\r\n              \"size\": \"Standard_F2\",\r\n              \"admin_ssh_key\": [\r\n                {\r\n                  \"public_key\": \"ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAAAgQDArSXQCqoc3aPBDiV2FkY2Sw+DkfVAJbxdE3kDIWShwW6AGQP+Lx420yltx0EzeoLKj49IQcZteTUzy05icrYNtfJB5ulWuf41e1WW99QjoP5zZIlgl1AGKhkiOsQCwjna5ykzegMH2nbn93OT02Xdr117J29/Ef0ndUPOIEuyuw== noname\",\r\n                  \"username\": \"adminuser\"\r\n                }\r\n              ],\r\n              \"network_interface_ids\": [\r\n                \"${azurerm_network_interface.example.id}\"\r\n              ],\r\n              \"os_disk\": [\r\n                {\r\n                  \"caching\": \"ReadWrite\",\r\n                  \"storage_account_type\": \"Standard_LRS\"\r\n                }\r\n              ],\r\n              \"source_image_reference\": [\r\n                {\r\n                  \"offer\": \"UbuntuServer\",\r\n                  \"publisher\": \"Canonical\",\r\n                  \"sku\": \"16.04-LTS\",\r\n                  \"version\": \"latest\"\r\n                }\r\n              ]\r\n            }\r\n          ]\r\n        }\r\n      ]\r\n    }";

            Assert
                .That(
                    actual,
                    Is.EqualTo(expect));
        }
    }
}