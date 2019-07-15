using System;
using System.Threading.Tasks;
using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SecurityPermissionRepository : Repository<SecurityPermission, AppDbContext>, ISecurityPermissionRepository
    {
        public SecurityPermissionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> VerifyPermission(Guid authorizedId, Guid objectId)
        {
            var spec = SecurityPermissionSpecs.GetByAuthorizedIdSpec(authorizedId)
                .And(SecurityPermissionSpecs.GetByObjectIdSpec(objectId));
            var result = await GetSingleAsync(spec);
            return result != null;
        }
    }
}