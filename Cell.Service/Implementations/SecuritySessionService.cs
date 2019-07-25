using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SecuritySessionEntity;
using Cell.Model.Models.SecuritySession;
using System;
using System.Threading.Tasks;

namespace Cell.Service.Implementations
{
    public class SecuritySessionService : Service<SecuritySession, AppDbContext>, ISecuritySessionService
    {
        public SecuritySessionService(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> VerifySession(Guid id)
        {
            var spec = SecuritySessionSpecs.GetBySessionIdSpec(id);
            var result = await GetSingleAsync(spec);
            if (result == null) return false;
            var timeExists = (result.ExpiredTime - result.SigninTime).Seconds;
            return timeExists > 0;
        }
    }
}