using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Azure;
using Azure.Data.Tables;
using funcInfrastructureAsCode.Functions.Abstraction;
using funcInfrastructureAsCode.Functions.Commands;

namespace funcInfrastructureAsCode.Functions.DbModels
{
    public class ResourceGroup
         : ITableEntity,
           IMappedEntity<ResourceGroup>
    {
        public ResourceGroup()
        { }

        public string Name { get; set; }
        public string LocalName { get; set; }
        public string Location { get; set; }
        public string InfrastructureRequestId { get; set; }
        public string Status { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

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
                            new { location = Location, name = Name }
                            }
                        );

                return dict;
            }
        }

        public void Map(
            CreateVirtualMachineCommand command)
        {
            RowKey = Guid.NewGuid().ToString("n");
            Name = command.ResourceGroup.Name;
            LocalName = command.ResourceGroup.LocalName;
            Location = command.ResourceGroup.Location;
            PartitionKey = command.ResourceGroup.Name;
            InfrastructureRequestId = $"{command.Id.Value}";
            Status = "Pending";
        }
    }
}