using Cell.Core.Constants;
using Cell.Core.Extensions;
using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SettingFeatureAggregate;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Infrastructure.Repositories
{
    public class SettingFeatureRepository : Repository<SettingFeature, AppDbContext>, ISettingFeatureRepository
    {
        private readonly string _connectionString;

        public SettingFeatureRepository(AppDbContext dbContext, IConfiguration configuration) : base(dbContext)
        {
            _connectionString = configuration.GetConnectionString(ConfigurationKeys.DefaultConnection);
        }

        public async Task<bool> AnyAsync()
        {
            return await _dbContext.SettingFeatures.AnyAsync();
        }

        public async Task<List<SettingFeature>> GetTreeAsync()
        {
            var settingFeatureResults = await _dbContext.SettingFeatures.ToListAsync();
            var result = BuildTree(null, settingFeatureResults.To<List<SettingFeature>>());
            return result;
        }

        private List<SettingFeature> BuildTree(Guid? settingFeatureParentId, List<SettingFeature> source)
        {
            return source.Where(item =>
                (settingFeatureParentId == null && (item.Parent == Guid.Empty)) ||
                (item.Parent == settingFeatureParentId)).Select(settingFeature => new SettingFeature
                {
                    Id = settingFeature.Id,
                    Name = settingFeature.Name,
                    Code = settingFeature.Code,
                    Description = settingFeature.Description,
                    Created = settingFeature.Created,
                    Modified = settingFeature.Modified,
                    Settings = settingFeature.Settings,
                    Children = BuildTree(settingFeature.Id, source).ToList(),
                }).ToList();
        }
    }
}