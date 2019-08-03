using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SecurityUserEntity
{
    [Table("T_SECURITY_USER")]
    public class SecurityUser : Entity
    {
        [Column("ACCOUNT")]
        [StringLength(200)]
        public string Account { get; set; }

        [Column("DEFAULT_DEPARTMENT")]
        public Guid DefaultDepartment { get; set; }

        [Column("DEFAULT_ROLE")]
        public Guid DefaultRole { get; set; }

        [Column("EMAIL")]
        [StringLength(200)]
        public string Email { get; set; }

        [Column("ENCRYPTED_PASSWORD")]
        [StringLength(1000)]
        public string EncryptedPassword { get; set; }

        [Column("PHONE")]
        [StringLength(50)]
        public string Phone { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("STATUS")]
        public Guid Status { get; set; }
    }
}