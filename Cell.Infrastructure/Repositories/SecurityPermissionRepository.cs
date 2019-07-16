using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Infrastructure.Repositories
{
    public class SecurityPermissionRepository : Repository<SecurityPermission, AppDbContext>, ISecurityPermissionRepository
    {
        public SecurityPermissionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Guid>> QueryByTable(string tableName, Guid groupId)
        {
            var spec = SecurityPermissionSpecs.GetByTableTargetSpec(tableName)
                .And(SecurityPermissionSpecs.GetByGroupIdSpec(groupId));
            var result = await GetManyAsync(spec);
            return result.Select(x => x.ObjectId).ToList();
        }
    }
}