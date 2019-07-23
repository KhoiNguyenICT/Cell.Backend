namespace Cell.Application.Api.Models.SettingActionInstance
{
    public class SettingActionInstanceUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SettingActionInstanceSettingsModel Settings { get; set; }
    }
}