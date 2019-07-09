using Cell.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Domain.Aggregates.SecuritySessionAggregate
{
    [Table("T_SECURITY_SESSION")]
    public class SecuritySession : Entity, IAggregateRoot
    {
        [Column("EXPIRED_TIME")]
        public int ExpiredTime { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("SIGNIN_TIME")]
        public DateTimeOffset SigninTime { get; set; }

        [Column("USER_ID")]
        public Guid UserId { get; set; }

        [Column("USER_ACCOUNT")]
        [StringLength(200)]
        public string UserAccount { get; set; }
    }
}