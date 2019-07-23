using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SecurityUserEntity;

namespace Cell.Service.Implementations
{
    public class SecurityUserService : Service<SecurityUser, AppDbContext>, ISecurityUserService
    {
        public SecurityUserService(AppDbContext context) : base(context)
        {
        }
    }
}