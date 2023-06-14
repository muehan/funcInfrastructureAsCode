using System;
using Azure;
using Azure.Data.Tables;

namespace funcInfrastructureAsCode.Functions.DbModels
{
    public class InfrastructureRequest
        : ITableEntity
    {
        public Guid Id { get; set; }
        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public string RequestStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}