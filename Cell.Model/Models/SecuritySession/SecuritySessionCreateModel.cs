using System;

namespace Cell.Model.Models.SecuritySession
{
    public class SecuritySessionCreateModel
    {
        public DateTimeOffset ExpiredTime { get; set; }
        public DateTimeOffset SigninTime { get; set; }
        public Guid UserId { get; set; }
        public string UserAccount { get; set; }
        public SecuritySessionSettingsModel Settings { get; set; }
    }
}