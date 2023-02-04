using System;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using Microsoft.Extensions.Logging;

namespace funcInfrastructureAsCode.Functions.Interfaces
{
    public class GitInterface
    {
        public Branch CreateBranch(
           Repository repo,
           ILogger log)
        {
            log.LogInformation($"CreateBranch");
            var random = new Random();
            var branch = repo.CreateBranch($"resourcegroup-{random.Next(1000, 9999)}");
            LibGit2Sharp.Commands.Checkout(repo, branch);

            return branch;
        }

        public void StageChanges(
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

        public void PushChanges(
            Repository repo,
            Branch branch,
            ILogger log)
        {
            log.LogInformation($"PushChanges");
            try
            {
                var password = Environment
                    .GetEnvironmentVariable(
                        "github_pat",
                        EnvironmentVariableTarget.Process);

                var remote = repo
                    .Network
                    .Remotes["origin"];

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

        public void CommitChanges(
            Repository repo,
            ILogger log)
        {
            log.LogInformation($"CommitChanges");

            var email = Environment
                    .GetEnvironmentVariable(
                        "github_email",
                        EnvironmentVariableTarget.Process);

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
    }
}