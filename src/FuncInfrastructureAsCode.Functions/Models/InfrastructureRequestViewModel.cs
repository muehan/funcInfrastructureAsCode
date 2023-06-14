using System;
using Newtonsoft.Json;

namespace funcInfrastructureAsCode.Functions.Models
{
    public class InfrastructureRequestViewModel
    {
        public string RowKey { get; set; }
        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public string RequestStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class InfrastructureRequestCreateModel
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string RequesterName { get; set; }
        public string RequesterEmail { get; set; }
        public string RequestStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}