using System;
using Azure;
using Azure.Data.Tables;

namespace funcInfrastructureAsCode.Functions.DbModels
{
    public class VirtualNetwork
        : ITableEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string ResourceGroupName { get; set; }
        public string AddressSpace { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        /*
            Expected Output
            {
                "address_space": [
                    "10.0.0.0/16"
                ],
                "location": "West Europe",
                "name": "dev-network",
                "resource_group_name": "DevelopmentResource"
            }
        */
        public dynamic TerraFormStructure => new
        {
            address_space = new[] { AddressSpace },
            location = Location,
            name = Name,
            resource_group_name = ResourceGroupName
        };
    }
}