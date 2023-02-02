using System;
using Azure;
using Azure.Data.Tables;

namespace funcInfrastructureAsCode.Functions.DbModels
{
    public class Subnet
        : ITableEntity
    {
        public string Name { get; set; }
        public string ResourceGroupName { get; set; }
        public string AddressPrefixes { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string VirtualNetworkName { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }


        /* Expected output
        {
            "address_prefixes": [
                "10.0.2.0/24"
            ],
            "name": self._subnetName,
            "resource_group_name": self._resourceGroup._name,
            "virtual_network_name": "${azurerm_virtual_network.example.name}"
        }
        */
        public dynamic TerraFormStructure => new
        {
            address_prefixes = new[] { AddressPrefixes },
            name = Name,
            resource_group_name = ResourceGroupName,
            virtual_network_name = VirtualNetworkName
        };
    }
}