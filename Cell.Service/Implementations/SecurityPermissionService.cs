using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SecurityPermissionEntity;

namespace Cell.Service.Implementations
{
    public class SecurityPermissionService : Service<SecurityPermission, AppDbContext>, ISecurityPermissionService
    {
        public SecurityPermissionService(AppDbContext context) : base(context)
        {
        }
    }
}