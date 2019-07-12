using System;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SecurityUserAggregate
{
    public class SecurityUserModel : Entity
    {
        public string Account { get; set; }

        public Guid DefaultDepartment { get; set; }

        public Guid DefaultRole { get; set; }

        public string Email { get; set; }

        public string EncryptedPassword { get; set; }

        public string Phone { get; set; }

        public string Settings { get; set; }

        public Guid Status { get; set; }
    }
}