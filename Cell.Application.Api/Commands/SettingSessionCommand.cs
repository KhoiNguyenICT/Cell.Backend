using System;

namespace Cell.Application.Api.Commands
{
    public class SettingSessionCommand : Command
    {
        public int ExpiredTime { get; set; }

        public string Settings { get; set; }

        public DateTimeOffset SigninTime { get; set; }

        public Guid UserId { get; set; }

        public string UserAccount { get; set; }
    }

    public class SettingSessionSettingCommand
    {
        public string Token { get; set; }
        public string UserAgent { get; set; }
        public string Os { get; set; }
        public string Browser { get; set; }
        public string Device { get; set; }
        public string OsVersion { get; set; }
        public string BrowserVersion { get; set; }
        public bool IsDesktop { get; set; }
        public bool IsMobile { get; set; }
        public bool IsTablet { get; set; }
    }
}