using Cell.Core.Constants;
using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SecurityGroupAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Infrastructure.Repositories
{
    public class SecurityGroupRepository : Repository<SecurityGroup, AppDbContext>, ISecurityGroupRepository
    {
        public SecurityGroupRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> AnyAsync()
        {
            return await _dbContext.SecurityGroups.AnyAsync();
        }

        public async Task<List<SecurityGroup>> GetTreeAsync(string code)
        {
            var settingGroupRoot = await _dbContext.SecurityGroups.Where(x => x.Code == code).ToListAsync();
            var settingGroupResults = new List<SecurityGroup>();
            settingGroupResults.AddRange(settingGroupRoot);
            switch (code)
            {
                case ConfigurationKeys.SystemRole:
                    {
                        var groups = await _dbContext.SecurityGroups
                            .Where(x => x.Code == ConfigurationKeys.Role).ToListAsync();
                        settingGroupResults.AddRange(groups);
                        break;
                    }

                case ConfigurationKeys.SystemDepartment:
                    {
                        var groups = await _dbContext.SecurityGroups
                            .Where(x => x.Code == ConfigurationKeys.Department).ToListAsync();
                        settingGroupResults.AddRange(groups);
                        break;
                    }

                case ConfigurationKeys.Deleted:
                    {
                        var groups = await _dbContext.SecurityGroups
                            .Where(x => x.Code == ConfigurationKeys.Deleted).ToListAsync();
                        settingGroupResults.AddRange(groups);
                        break;
                    }
            }

            var result = BuildTree(null, settingGroupResults);
            return result;
        }

        private List<SecurityGroup> BuildTree(Guid? securityGroupParentId, List<SecurityGroup> source)
        {
            return source.Where(item =>
                (securityGroupParentId == null && (item.Parent == Guid.Empty)) ||
                (item.Parent == securityGroupParentId)).Select(securityGroup => new SecurityGroup
                {
                    Id = securityGroup.Id,
                    Name = securityGroup.Name,
                    Code = securityGroup.Code,
                    Description = securityGroup.Description,
                    Created = securityGroup.Created,
                    Modified = securityGroup.Modified,
                    Settings = securityGroup.Settings,
                    Parent = securityGroup.Parent,
                    Children = BuildTree(securityGroup.Id, source).ToList(),
                }).ToList();
        }
    }
}