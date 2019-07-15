using System;
using System.Threading.Tasks;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SecurityPermissionAggregate
{
    public interface ISecurityPermissionRepository : IRepository<SecurityPermission>
    {
        Task<bool> VerifyPermission(Guid authorizedId, Guid objectId);
    }
}