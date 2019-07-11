namespace Cell.Application.Api.Commands.Others
{
    public class LoginCommand
    {
        public string Account { get; set; }
        public string Password { get; set; }
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