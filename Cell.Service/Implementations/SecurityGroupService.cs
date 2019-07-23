using Cell.Common.SeedWork;
using Cell.Model;
using Cell.Model.Entities.SecurityGroupEntity;

namespace Cell.Service.Implementations
{
    public class SecurityGroupService : Service<SecurityGroup, AppDbContext>, ISecurityGroupService
    {
        public SecurityGroupService(AppDbContext context) : base(context)
        {
        }
    }
}