using Cell.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Domain.Aggregates.SecurityUserAggregate
{
    [Table("T_SECURITY_USER")]
    public class SecurityUser : Entity, IAggregateRoot
    {
        [Column("ACCOUNT")]
        [StringLength(200)]
        public string Account { get; private set; }

        [Column("DEFAULT_DEPARTMENT")]
        public Guid DefaultDepartment { get; private set; }

        [Column("DEFAULT_ROLE")]
        public Guid DefaultRole { get; private set; }

        [Column("EMAIL")]
        [StringLength(200)]
        public string Email { get; private set; }

        [Column("ENCRYPTED_PASSWORD")]
        [StringLength(1000)]
        public string EncryptedPassword { get; private set; }

        [Column("PHONE")]
        [StringLength(50)]
        public string Phone { get; private set; }

        [Column("SETTINGS")]
        public string Settings { get; private set; }

        [Column("STATUS")]
        public Guid Status { get; private set; }

        public SecurityUser() { }

        public SecurityUser(
            string code,
            string description,
            string account,
            string email,
            string encryptedPassword,
            string phone,
            string settings)
        {
            Code = code;
            Description = description;
            Account = account;
            Email = email;
            EncryptedPassword = encryptedPassword;
            Phone = phone;
            Settings = settings;
        }

        public void Update(
            string description,
            string email,
            string phone,
            string settings)
        {
            Description = description;
            Email = email;
            Phone = phone;
            Settings = settings;
        }

        public void UpdateGroup(
            Guid defaultRole,
            Guid defaultDepartment,
            string settings)
        {
            DefaultRole = defaultRole;
            DefaultDepartment = defaultDepartment;
            Settings = settings;
        }

        public void ChangeStatus(Guid status)
        {
            Status = status;
        }
    }
}