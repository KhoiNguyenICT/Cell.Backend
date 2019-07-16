using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SecurityPermissionAggregate
{
    public interface ISecurityPermissionRepository : IRepository<SecurityPermission>
    {
        Task<List<Guid>> QueryByTable(string tableName, Guid groupId);
    }
}