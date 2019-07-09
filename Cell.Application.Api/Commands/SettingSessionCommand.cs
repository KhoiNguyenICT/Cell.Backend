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
}