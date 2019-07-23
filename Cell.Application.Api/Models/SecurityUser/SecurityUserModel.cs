using System;
using Cell.Common.SeedWork;

namespace Cell.Application.Api.Models.SecurityUser
{
    public class SecurityUserModel : BaseModel
    {
        public string Account { get; set; }

        public Guid DefaultDepartment { get; set; }

        public Guid DefaultRole { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public SecurityUserSettingsModel Settings { get; set; }

        public Guid Status { get; set; }
    }
}