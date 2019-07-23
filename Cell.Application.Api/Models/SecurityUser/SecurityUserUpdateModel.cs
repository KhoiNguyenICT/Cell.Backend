namespace Cell.Application.Api.Models.SecurityUser
{
    public class SecurityUserUpdateModel
    {
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public SecurityUserSettingsModel Settings { get; set; }
    }
}