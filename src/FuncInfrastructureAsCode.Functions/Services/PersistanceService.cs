using System;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using funcInfrastructureAsCode.Functions.Abstraction;
using funcInfrastructureAsCode.Functions.Commands;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace funcInfrastructureAsCode.Functions.Services
{
    public class PersistanceService
    {
        private readonly ILogger _logger;

        public PersistanceService(
            ILogger logger)
        {
            _logger = logger;
        }

        public async Task<T> CreateIfNotExist<T>(
           IAsyncCollector<T> tableCollector,
           TableClient tableClient,
           CreateVirtualMachineCommand command,
           Func<T, bool> find)
           where T : class,
                     IMappedEntity<T>,
                     ITableEntity,
                     new()
        {
            var entities = tableClient
                .Query<T>()
                .ToList();

            var nameExist = entities
                .Any(
                    e => find(e));

            _logger
                .LogInformation(
                    $"does entity exist? {nameExist}");

            if (!nameExist)
            {
                var entity = (T)Activator
                    .CreateInstance(
                        typeof(T));

                _logger
                    .LogInformation(
                        $"creating entity {typeof(T).Name}");

                entity
                    .Map(
                        command);

                _logger.LogInformation(
                    $"adding entity {typeof(T).Name}");

                await tableCollector
                    .AddAsync(
                        entity);

                _logger
                    .LogInformation(
                        $"entity {typeof(T).Name} added");

                return entity;
            }

            return default(T);
        }
    }
}