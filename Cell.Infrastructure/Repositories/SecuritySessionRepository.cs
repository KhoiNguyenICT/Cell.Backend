using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SecuritySessionAggregate;

namespace Cell.Infrastructure.Repositories
{
    public class SecuritySessionRepository : Repository<SecuritySession, AppDbContext>, ISecuritySessionRepository
    {
        public SecuritySessionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}