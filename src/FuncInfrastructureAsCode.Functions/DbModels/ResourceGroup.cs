using System;
using Azure;
using Azure.Data.Tables;

namespace funcInfrastructureAsCode.Functions.DbModels
{
    public class ResourceGroup
         : ITableEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public dynamic TerraFormStructure => new { location = Location, name = Name };
    }
}