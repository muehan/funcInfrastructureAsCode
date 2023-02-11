using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Azure;
using Azure.Data.Tables;

namespace funcInfrastructureAsCode.Functions.DbModels
{
    public class NetworkInterface
        : ITableEntity
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string Location { get; set; }
        public string ResourceGroupName { get; set; }
        public string IpConfiguratioName { get; set; }
        public string IpConfiguratioPrivateIpAddressAllocation { get; set; }
        public string IpConfiguratioSubnetId { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        /*
        {
            "name": "dev-interface",
            "location": "West Europe",
            "resource_group_name": "DevelopmentResource",
            "ip_configuration": [
                {
                    "name": "internal",
                    "private_ip_address_allocation": "Dynamic",
                    "subnet_id": "${azurerm_subnet.example.id}"
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
                            new {
                                name = Name,
                                location = Location,
                                resource_group_name = ResourceGroupName,
                                ip_configuration = new[] {
                                    new
                                    {
                                        name = IpConfiguratioName,
                                        private_ip_address_allocation = IpConfiguratioPrivateIpAddressAllocation,
                                        subnet_id = IpConfiguratioSubnetId
                                    }}
                                }
                            }
                    );

                return dict;
            }
        }
    }
}