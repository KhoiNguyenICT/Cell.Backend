using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SecurityPermissionAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SecurityPermissionRepository : Repository<SecurityPermission, AppDbContext>, ISecurityPermissionRepository
    {
        public SecurityPermissionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}