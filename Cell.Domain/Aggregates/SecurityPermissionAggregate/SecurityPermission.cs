using Cell.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Domain.Aggregates.SecurityPermissionAggregate
{
    [Table("T_SECURITY_PERMISSION")]
    public class SecurityPermission : Entity, IAggregateRoot
    {
        [Column("AUTHORIZED_ID")]
        public Guid AuthorizedId { get; private set; }

        [Column("AUTHORIZED_TYPE")]
        [StringLength(200)]
        public string AuthorizedType { get; private set; }

        [Column("OBJECT_ID")]
        public Guid ObjectId { get; private set; }

        [Column("OBJECT_NAME")]
        [StringLength(200)]
        public string ObjectName { get; private set; }

        [Column("SETTINGS")]
        public string Settings { get; private set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; private set; }

        public SecurityPermission(
            Guid authorizedId,
            string authorizedType,
            Guid objectId,
            string objectName,
            string settings,
            string tableName)
        {
            AuthorizedId = authorizedId;
            AuthorizedType = authorizedType;
            ObjectId = objectId;
            ObjectName = objectName;
            Settings = settings;
            TableName = tableName;
        }
    }
}