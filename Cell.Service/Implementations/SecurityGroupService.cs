using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<List<SecurityGroup>> GetTreeAsync(string code)
        {
            throw new System.NotImplementedException();
        }
    }
}