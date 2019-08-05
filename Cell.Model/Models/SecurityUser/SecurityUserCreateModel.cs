namespace Cell.Model.Models.SecurityUser
{
    public class SecurityUserCreateModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Account { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public SecurityUserSettingsModel Settings { get; set; }
    }
}