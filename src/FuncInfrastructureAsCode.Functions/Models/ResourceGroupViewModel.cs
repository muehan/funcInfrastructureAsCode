using System;
using System.Collections.Generic;

namespace funcInfrastructureAsCode.Functions.Models
{
    public class ResourceGroupViewModel
    {
        public string Name { get; set; }
        public string LocalName { get; set; }
        public string Location { get; set; }

        internal void Validate(
            List<string> errors)
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                errors.Add("ResourceGroup.Name is required");
            }

            if(string.IsNullOrWhiteSpace(LocalName))
            {
                errors.Add("ResourceGroup.LocalName is required");
            }

            if(string.IsNullOrWhiteSpace(Location))
            {
                errors.Add("ResourceGroup.Location is required");
            }
        }
    }
}