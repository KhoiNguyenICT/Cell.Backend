using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cell.Common.SeedWork;

namespace Cell.Model.Entities.SecurityPermissionEntity
{
    public interface ISecurityPermissionService : IService<SecurityPermission>
    {
        Task<List<Guid>> GetGroupIdsByAccountId(Guid accountId);

        Task InitPermission(Guid objectId, string objectName, string authorizedType);
    }
}