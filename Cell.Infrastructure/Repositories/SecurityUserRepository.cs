using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SecurityUserAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SecurityUserRepository : Repository<SecurityUser, AppDbContext>, ISecurityUserRepository
    {
        public SecurityUserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}