using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace funcInfrastructureAsCode
{
    public static class AddResourceGroup
    {
        [FunctionName("AddResourceGroup")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string name = data?.name;
            string location = data?.location;

            var resource = new ResourceGroup {
                name = name,
                location = location
            };

            return new OkObjectResult(JsonConvert.SerializeObject(resource));
        }
    }

    public struct ResourceGroup
    {
        public string name { get; set; }
        public string location { get; set; }
    }
}
