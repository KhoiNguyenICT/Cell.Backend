using System;
using Cell.Common.SeedWork;

namespace Cell.Model.Models.SecuritySession
{
    public class SecuritySessionModel : BaseModel
    {
        public DateTimeOffset ExpiredTime { get; set; }

        public SecuritySessionSettingsModel Settings { get; set; }

        public DateTimeOffset SigninTime { get; set; }

        public Guid UserId { get; set; }

        public string UserAccount { get; set; }
    }
}