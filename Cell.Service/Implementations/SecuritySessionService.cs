using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SecuritySessionEntity;

namespace Cell.Service.Implementations
{
    public class SecuritySessionService : Service<SecuritySession, AppDbContext>,ISecuritySessionService
    {
        public SecuritySessionService(AppDbContext context) : base(context)
        {
        }
    }
}