using System.Collections.Generic;
using System.Threading.Tasks;
using Cell.Common.SeedWork;

namespace Cell.Model.Entities.SecurityGroupEntity
{
    public interface ISecurityGroupService : IService<SecurityGroup>
    {
        Task<List<SecurityGroup>> GetTreeAsync(string code);
    }
}