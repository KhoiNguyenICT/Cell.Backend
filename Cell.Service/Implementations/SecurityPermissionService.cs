using Cell.Common.Constants;
using Cell.Model.Entities.SecurityGroupEntity;
using Cell.Model.Entities.SecurityPermissionEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cell.Service.Implementations
{
    public class SecurityPermissionService : Service<SecurityPermission>, ISecurityPermissionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Guid CurrentAccountId => Guid.Parse(_httpContextAccessor.HttpContext.Request?.Headers["Account"] ?? throw new InvalidOperationException());

        public SecurityPermissionService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Guid>> GetGroupIdsByAccountId(Guid accountId)
        {
            var groupIds = new List<Guid>();
            var idLeftIndexRightIndexItems = await (from securityGroup in Context.SecurityGroups
                                                    where (from permission in Context.SecurityPermissions
                                                           where permission.AuthorizedId == accountId &&
                                                                 permission.TableName == ConfigurationKeys.SecurityGroupTableName
                                                           select permission.ObjectId).Contains(securityGroup.Id)
                                                    select new SecurityGroupOutputByAccountModel
                                                    {
                                                        Id = securityGroup.Id,
                                                        IndexLeft = securityGroup.IndexLeft,
                                                        IndexRight = securityGroup.IndexRight
                                                    }).ToListAsync();
            groupIds.AddRange(idLeftIndexRightIndexItems.Select(x => x.Id));
            foreach (var idLeftIndexRightIndexItem in idLeftIndexRightIndexItems)
            {
                if (groupIds.IndexOf(idLeftIndexRightIndexItem.Id) == -1)
                    groupIds.AddRange((from contextSecurityGroup in Context.SecurityGroups
                                       where contextSecurityGroup.IndexLeft > idLeftIndexRightIndexItem.IndexLeft &&
                                             contextSecurityGroup.IndexRight < idLeftIndexRightIndexItem.IndexRight
                                       select contextSecurityGroup.Id));
            }

            return groupIds;
        }

        public Task InitPermission(Guid objectId, string objectName, string authorizedType)
        {
            throw new NotImplementedException();
        }
    }
}