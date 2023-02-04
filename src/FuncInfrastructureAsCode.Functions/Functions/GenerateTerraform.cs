using System;
using System.IO;
using System.Threading.Tasks;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Interfaces;
using LibGit2Sharp;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace funcInfrastructureAsCode.Functions.Functions
{
    public class GenerateTerraform
    {
        [FunctionName("GenerateTerraform")]
        public async Task Run(
            [QueueTrigger("terraformTrigger", Connection = "AzureWebJobsStorage")] string myQueueItem,
            [Table("RecourceGroup", Connection = "AzureWebJobsStorage")] IAsyncCollector<ResourceGroup> resourceGroupTable,
            [Table("Subnet", Connection = "AzureWebJobsStorage")] IAsyncCollector<Subnet> subnetTable,
            [Table("NetworkInterface", Connection = "AzureWebJobsStorage")] IAsyncCollector<NetworkInterface> netowrkInterfaceTable,
            [Table("VirtualNetwork", Connection = "AzureWebJobsStorage")] IAsyncCollector<VirtualNetwork> virtualNetworkTable,
            [Table("VirtualMachine", Connection = "AzureWebJobsStorage")] IAsyncCollector<VirtualMachine> virtualMachineTable,
            ILogger log)
        {
            var github = new GithubInterface();
            var git = new GitInterface();

            log.LogInformation($"Generate new Terraform File");
            var random = new Random();

            var tempPath = Path.GetTempPath();
            var repoPath = Path.Combine(tempPath, $"code{random.Next(1000, 9999)}");

            var repoUrl = GetEnvironmentVariable("github_repo");

            Repository.Clone(repoUrl, repoPath);

            using (var repo = new Repository(repoPath))
            {
                var branch = git.CreateBranch(repo, log);

                File
                    .AppendAllText(
                        Path.Combine(repoPath, "textfile.txt"),
                        "demo content");

                GenerateTerraFormFiles();

                git.StageChanges(repo, log);
                git.CommitChanges(repo, log);
                git.PushChanges(repo, branch, log);
                await github.CreatePullRequest(branch.FriendlyName, log);
            }
        }

        private static void GenerateTerraFormFiles()
        {
            
        }

        private static string GetEnvironmentVariable(
            string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}
