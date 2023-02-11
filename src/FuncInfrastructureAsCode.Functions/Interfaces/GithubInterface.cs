using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace funcInfrastructureAsCode.Functions.Interfaces
{
    public class GithubInterface
    {
        public async Task CreatePullRequest(
           string branchName,
           ILogger log)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.github.com/repos/muehan/terraform_autogenerated/pulls"))
                {
                    var pat = Environment
                        .GetEnvironmentVariable(
                            "github_pat",
                            EnvironmentVariableTarget.Process);

                    request.Headers.TryAddWithoutValidation("Accept", "application/vnd.github+json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {pat}");
                    request.Headers.TryAddWithoutValidation("X-GitHub-Api-Version", "2022-11-28");
                    request.Headers.TryAddWithoutValidation("User-Agent", "terraform_autogenerated");

                    request.Content = new StringContent("{\"title\":\"New Resource Group created " + branchName + "\",\"body\":\"New Resource Group created\",\"head\":\"" + branchName + "\",\"base\":\"master\"}");

                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);

                    log.LogInformation($"Create PullRequest: {response}");
                }
            }
        }
    }
}