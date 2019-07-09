using System.Collections.Generic;
using System.Threading.Tasks;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SecurityGroupAggregate
{
    public interface ISecurityGroupRepository : IRepository<SecurityGroup>
    {
        Task<bool> AnyAsync();
        Task<List<SecurityGroup>> GetTreeAsync(string code);
    }
}