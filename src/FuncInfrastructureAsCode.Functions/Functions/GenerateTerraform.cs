using System.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using funcInfrastructureAsCode.Functions.Builder;
using funcInfrastructureAsCode.Functions.DbModels;
using funcInfrastructureAsCode.Functions.Interfaces;
using LibGit2Sharp;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Azure.Data.Tables;

namespace funcInfrastructureAsCode.Functions.Functions
{
    public class GenerateTerraform
    {
        [FunctionName("GenerateTerraform")]
        public async Task Run(
            [QueueTrigger("terraformTrigger", Connection = "AzureWebJobsStorage")] string myQueueItem,
            [Table("RecourceGroup", Connection = "AzureWebJobsStorage")] TableClient resourceGroupTable,
            [Table("Subnet", Connection = "AzureWebJobsStorage")] TableClient subnetTable,
            [Table("NetworkInterface", Connection = "AzureWebJobsStorage")] TableClient netowrkInterfaceTable,
            [Table("VirtualNetwork", Connection = "AzureWebJobsStorage")] TableClient virtualNetworkTable,
            [Table("VirtualMachine", Connection = "AzureWebJobsStorage")] TableClient virtualMachineTable,
            ILogger log)
        {
            log.LogInformation($"GenerateTerraform trigger with {myQueueItem}");

            var github = new GithubInterface();
            var git = new GitInterface();

            log.LogInformation($"Generate new Terraform File");
            var random = new Random();

            var tempPath = Path
                .GetTempPath();

            var repoPath = Path
                .Combine(
                    tempPath,
                    $"code{myQueueItem}");

            var repoUrl = GetEnvironmentVariable(
                "github_repo");

            Repository
                .Clone(
                    repoUrl,
                    repoPath);

            var resourceGroupList = resourceGroupTable
                .Query<ResourceGroup>()
                .Where(
                    x =>
                    x.Status == "Active" ||
                    x.InfrastructureRequestId == myQueueItem)
                .ToList();

            var virtualnetworkList = virtualNetworkTable
                .Query<VirtualNetwork>()
                .Where(
                    x =>
                    x.Status == "Active" ||
                    x.InfrastructureRequestId == myQueueItem)
                .ToList();

            var subnetList = subnetTable
                .Query<Subnet>()
                .Where(
                    x =>
                    x.Status == "Active" ||
                    x.InfrastructureRequestId == myQueueItem)
                .ToList();

            var networkInterfaceList = netowrkInterfaceTable
                .Query<NetworkInterface>()
                .Where(
                    x =>
                    x.Status == "Active" ||
                    x.InfrastructureRequestId == myQueueItem)
                .ToList();

            var virtualMachineList = virtualMachineTable
                .Query<VirtualMachine>()
                .Where(
                    x =>
                    x.Status == "Active" ||
                    x.InfrastructureRequestId == myQueueItem)
                .ToList();

            using (var repo = new Repository(repoPath))
            {
                var branch = git.CreateBranch(repo, log);

                GenerateTerraFormFiles(
                    repoPath,
                    resourceGroupList,
                    virtualnetworkList,
                    subnetList,
                    networkInterfaceList,
                    virtualMachineList,
                    log);

                git.StageChanges(repo, log);
                git.CommitChanges(repo, log);
                git.PushChanges(repo, branch, log);
                await github.CreatePullRequest(branch.FriendlyName, log);
            }

            Directory.Delete(
                repoPath,
                true);
        }

        private static void GenerateTerraFormFiles(
            string repoPath,
            List<ResourceGroup> resourceGroupList,
            List<VirtualNetwork> virtualNetworkList,
            List<Subnet> subnetList,
            List<NetworkInterface> networkInterfaceList,
            List<VirtualMachine> virtualMachineList,
            ILogger log)
        {
            log.LogInformation($"Start generating file");
            var terraformFileBuilder = new TerraformFileBuilder();

            var fileContent = terraformFileBuilder
                .Create(
                    resourceGroupList,
                    virtualNetworkList,
                    subnetList,
                    networkInterfaceList,
                    virtualMachineList,
                    log);

            var environmentPath = Path.Combine(
                        repoPath,
                        GetEnvironmentVariable("environment"));

            log.LogInformation($"Start terraform file for {environmentPath}");

            if (!Directory.Exists(environmentPath))
            {
                Directory.CreateDirectory(environmentPath);
            }

            File
                .WriteAllText(
                    Path.Combine(
                        repoPath,
                        GetEnvironmentVariable("environment"),
                        "main.tf.json"),
                    fileContent);
        }

        private static string GetEnvironmentVariable(
            string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}
