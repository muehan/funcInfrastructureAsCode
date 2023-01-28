using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibGit2Sharp;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace funcInfrastructureAsCode
{
    public class GenerateTerraform
    {
        [FunctionName("GenerateTerraform")]
        public void Run(
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

                StageChanges(repo, log);
                CommitChanges(repo, log);
                PushChanges(repo, branch, log);
            }
        }

        public static Branch CreateBranch(
            Repository repo,
            ILogger log)
        {
            log.LogInformation($"CreateBranch");
            var random = new Random();
            var branch = repo.CreateBranch($"resourcegroup-{random.Next(1000, 9999)}");
            Commands.Checkout(repo, branch);

            return branch;
        }

        public static void StageChanges(
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

        public static void CommitChanges(
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

        public static void PushChanges(
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
            }
        }

        private static string GetEnvironmentVariable(
            string name)
        {
            return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
        }
    }
}
