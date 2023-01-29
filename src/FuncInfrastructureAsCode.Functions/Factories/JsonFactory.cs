using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace funcInfrastructureAsCode.Functions.Factories
{
    public class JsonFactory
    {
        private readonly JsonSerializerSettings settings;

        public JsonFactory()
        {
            settings = new JsonSerializerSettings();
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public string Create(Object target)
        {
            string json = JsonConvert
               .SerializeObject(
                   target,
                   Formatting.Indented,
                   settings);

            return json;
        }
    }
}