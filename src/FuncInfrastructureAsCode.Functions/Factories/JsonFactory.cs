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
            settings.Formatting = Formatting.Indented;
        }

        public string Create(
            Object target)
        {
            string json = JsonConvert
               .SerializeObject(
                   target,
                   settings);

            return json;
        }
    }
}