using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Security.Claims;

namespace FuncInfrastructureAsCode.Functions.Functions
{
    public static class Authtest
    {
        [FunctionName("Authtest")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var sb = new StringBuilder();
            var identity = req.HttpContext?.User?.Identity as ClaimsIdentity;
            sb.AppendLine($"IsAuthenticated: {identity?.IsAuthenticated}");
            sb.AppendLine($"Identity name: {identity?.Name}");
            sb.AppendLine($"AuthenticationType: {identity?.AuthenticationType}");
            foreach (var claim in identity?.Claims)
            {
                sb.AppendLine($"Claim: {claim.Type} : {claim.Value}");
            }
            return new OkObjectResult(sb.ToString());
        }
    }
}
