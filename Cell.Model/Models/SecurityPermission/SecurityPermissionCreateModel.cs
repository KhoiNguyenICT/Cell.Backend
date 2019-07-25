using System;

namespace Cell.Model.Models.SecurityPermission
{
    public class SecurityPermissionCreateModel
    {
        public Guid AuthorizedId { get; set; }
        public string AuthorizedType { get; set; }
        public Guid ObjectId { get; set; }
        public string ObjectName { get; set; }
        public string TableName { get; set; }
    }
}