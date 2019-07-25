using System;
using System.Collections.Generic;

namespace Cell.Model.Models.SecuritySession
{
    public class SecuritySessionSettingsModel
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
        public string Ip { get; set; }
        public List<Guid> GroupIds = new List<Guid>();
    }
}