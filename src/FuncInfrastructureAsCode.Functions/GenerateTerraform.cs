using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using LibGit2Sharp;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace funcInfrastructureAsCode
{
    public class GenerateTerraform
    {
        [FunctionName("GenerateTerraform")]
        public async Task Run(
            [QueueTrigger("terraformTrigger", Connection = "AzureWebJobsStorage")] string myQueueItem,
            ILogger log)
        {
            log.LogInformation($"Generate new Terraform File");
            var random = new Random();

            var tempPath = Path.GetTempPath();
            var repoPath = Path.Combine(tempPath, $"code{random.Next(1000, 9999)}");

            var repoUrl = GetEnvironmentVariable("github_repo");

            Repository.Clone(repoUrl, repoPath);

            using (var repo = new Repository(repoPath))
            {
                var branch = CreateBranch(repo, log);

                File
                    .AppendAllText(
                        Path.Combine(repoPath, "textfile.txt"),
                        "demo content");

                GenerateTerraFormFiles();

                StageChanges(repo, log);
                CommitChanges(repo, log);
                PushChanges(repo, branch, log);
                await CreatePullRequest(branch.FriendlyName, log);
            }
        }

        private static void GenerateTerraFormFiles(){

        }

        private static Branch CreateBranch(
            Repository repo,
            ILogger log)
        {
            log.LogInformation($"CreateBranch");
            var random = new Random();
            var branch = repo.CreateBranch($"resourcegroup-{random.Next(1000, 9999)}");
            Commands.Checkout(repo, branch);

            return branch;
        }

        private static void StageChanges(
            Repository repo,
            ILogger log)
        {
            log.LogInformation($"StageChanges");
            try
            {
                RepositoryStatus status = repo
                    .RetrieveStatus();

                List<string> filePaths = status
                    .Modified
                    .Select(
                        mods => mods.FilePath)
                    .ToList();

                foreach (var filePath in filePaths)
                {
                    repo.Index.Add(filePath);
                }

                filePaths = status
                    .Added
                    .Select(
                        mods => mods.FilePath)
                    .ToList();

                foreach (var filePath in filePaths)
                {
                    repo.Index.Add(filePath);
                }

                filePaths = status
                    .Untracked
                    .Select(
                        mods => mods.FilePath)
                    .ToList();

                foreach (var filePath in filePaths)
                {
                    repo.Index.Add(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:RepoActions:StageChanges " + ex.Message);
            }
        }

        private static void CommitChanges(
            Repository repo,
            ILogger log)
        {
            log.LogInformation($"CommitChanges");
            var email = GetEnvironmentVariable("github_email");

            try
            {
                repo
                    .Commit(
                        "updating files..",
                        new Signature("function", email, DateTimeOffset.Now),
                        new Signature("function", email, DateTimeOffset.Now));
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:RepoActions:CommitChanges " + e.Message);
            }
        }

        private static void PushChanges(
            Repository repo,
            Branch branch,
            ILogger log)
        {
            log.LogInformation($"PushChanges");
            try
            {
                var password = GetEnvironmentVariable("github_pat");
                var remote = repo.Network.Remotes["origin"];

                PushOptions options = new()
                {
                    CredentialsProvider = (url, usernameFromUrl, types) => new UsernamePasswordCredentials
                    {
                        Username = password,
                        Password = string.Empty
                    }
                };

                repo.Network.Push(
                    remote,
                    branch.CanonicalName,
                    options);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception:RepoActions:PushChanges " + e.Message);
                log.LogError("Exception:RepoActions:PushChanges " + e.Message);
            }
        }

        private static async Task CreatePullRequest(
            string branchName,
            ILogger log)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.github.com/repos/muehan/terraform_autogenerated/pulls"))
                {
                    request.Headers.TryAddWithoutValidation("Accept", "application/vnd.github+json");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {GetEnvironmentVariable("github_pat")}");
                    request.Headers.TryAddWithoutValidation("X-GitHub-Api-Version", "2022-11-28");
                    request.Headers.TryAddWithoutValidation("User-Agent", "terraform_autogenerated");

                    request.Content = new StringContent("{\"title\":\"New Resource Group created " + branchName + "\",\"body\":\"New Resource Group created\",\"head\":\"" + branchName + "\",\"base\":\"master\"}");

                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);

                    log.LogInformation($"Create PullRequest: {response}");
                }
            }
        }

        private static string GetEnvironmentVariable(
            string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}
