﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Cell.Domain.Aggregates.SettingFieldAggregate;
using Cell.Domain.Aggregates.SettingTableAggregate;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;

namespace Cell.Infrastructure
{
    public class AppDbContextSeed
    {
        private readonly AppDbContext _context;

        public AppDbContextSeed(AppDbContext context)
        {
            _context = context;
        }

        private string CreatePath(string jsonFile)
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

                    await InitSettingTable();
                    await InitSettingField();

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

        private async Task InitSettingTable()
        {
            if (!await _context.SettingTables.AnyAsync())
            {
                var input = File.ReadAllText(CreatePath("setting-table-data.json"));
                var settingTable = JsonConvert.DeserializeObject<List<SettingTable>>(input);
                _context.SettingTables.AddRange(settingTable);
            }
        }

        private async Task InitSettingField()
        {
            if (!await _context.SettingTables.AnyAsync())
            {
                var input = File.ReadAllText(CreatePath("setting-field-data.json"));
                var settingField = JsonConvert.DeserializeObject<List<SettingField>>(input);
                _context.SettingFields.AddRange(settingField);
            }
        }
    }
}