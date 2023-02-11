using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Azure;
using Azure.Data.Tables;

namespace funcInfrastructureAsCode.Functions.DbModels
{
    public class ResourceGroup
         : ITableEntity
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string Location { get; set; }
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
    }
}