using System;
using Cell.Common.SeedWork;

namespace Cell.Application.Api.Models.SecurityPermission
{
    public class SecurityPermissionModel: BaseModel
    {
        public Guid AuthorizedId { get; set; }

        public string AuthorizedType { get; set; }

        public Guid ObjectId { get; set; }

        public string ObjectName { get; set; }

        public string Settings { get; set; }

        public string TableName { get; set; }
    }
}