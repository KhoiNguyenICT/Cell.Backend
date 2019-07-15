using Cell.Core.SeedWork;
using Cell.Domain.Aggregates.SecuritySessionAggregate;
using System;
using System.Threading.Tasks;

namespace Cell.Infrastructure.Repositories
{
    public class SecuritySessionRepository : Repository<SecuritySession, AppDbContext>, ISecuritySessionRepository
    {
        public SecuritySessionRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> VerifySession(Guid id)
        {
            var spec = SecuritySessionSpecs.GetBySessionIdSpec(id);
            var result = await GetSingleAsync(spec);
            var timeExists = (result.ExpiredTime - DateTimeOffset.Now).Seconds;
            return timeExists > 0;
        }
    }
}