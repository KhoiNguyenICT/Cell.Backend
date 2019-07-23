using System;
using Cell.Common.SeedWork;

namespace Cell.Application.Api.Models.SecuritySession
{
    public class SecuritySessionModel : BaseModel
    {
        public int ExpiredTime { get; set; }

        public SecuritySessionSettingsModel Settings { get; set; }

        public DateTimeOffset SigninTime { get; set; }

        public Guid UserId { get; set; }

        public string UserAccount { get; set; }
    }
}