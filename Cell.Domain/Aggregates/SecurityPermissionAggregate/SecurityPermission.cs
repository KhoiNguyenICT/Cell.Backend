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
        public Guid AuthorizedId { get; set; }

        [Column("AUTHORIZED_TYPE")]
        [StringLength(200)]
        public string AuthorizedType { get; set; }

        [Column("OBJECT_ID")]
        public Guid ObjectId { get; set; }

        [Column("OBJECT_NAME")]
        [StringLength(200)]
        public string ObjectName { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; set; }
    }
}