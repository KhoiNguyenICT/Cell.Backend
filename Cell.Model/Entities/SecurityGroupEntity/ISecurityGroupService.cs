using Cell.Common.SeedWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cell.Model.Entities.SecurityGroupEntity
{
    public interface ISecurityGroupService : IService<SecurityGroup>
    {
        Task<List<SecurityGroup>> GetTreeAsync(string code);
    }
}