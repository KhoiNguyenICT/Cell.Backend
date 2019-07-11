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
        public DateTimeOffset ExpiredTime { get; private set; }

        [Column("SETTINGS")]
        public string Settings { get; private set; }

        [Column("SIGNIN_TIME")]
        public DateTimeOffset SigninTime { get; private set; }

        [Column("USER_ID")]
        public Guid UserId { get; private set; }

        [Column("USER_ACCOUNT")]
        [StringLength(200)]
        public string UserAccount { get; private set; }

        public SecuritySession(
            DateTimeOffset expiredTime,
            DateTimeOffset signinTime,
            Guid userId,
            string userAccount,
            string settings)
        {
            ExpiredTime = expiredTime;
            SigninTime = signinTime;
            UserId = userId;
            UserAccount = userAccount;
            Settings = settings;
        }
    }
}