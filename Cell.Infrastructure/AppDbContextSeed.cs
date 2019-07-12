using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace Cell.Infrastructure
{
    public class AppDbContextSeed
    {
        private readonly AppDbContext _context;

        public AppDbContextSeed(
            AppDbContext context)
        {
            _context = context;
        }

        private static string CreatePath(string jsonFile)
        {
            return "Setup/" + jsonFile;
        }

        public async Task SeedAsync(AppDbContext dbContext, IHostingEnvironment env, ILogger<AppDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(AppDbContext));
            await policy.ExecuteAsync(async () =>
            {
                using (dbContext)
                {
                    dbContext.Database.Migrate();
                    await InitSettingGroup();
                    await dbContext.SaveChangesAsync();
                }
            });
        }

        private static AsyncRetryPolicy CreatePolicy(ILogger logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                    }
                );
        }

        private async Task InitSettingGroup()
        {
            if (!await _context.SecurityGroups.AnyAsync())
            {
                var input = File.ReadAllText(CreatePath("setting-group-data.json"));
                var settingGroups = JsonConvert.DeserializeObject<List<SecurityGroup>>(input);
                _context.SecurityGroups.AddRange(settingGroups);
            }
        }
    }
}