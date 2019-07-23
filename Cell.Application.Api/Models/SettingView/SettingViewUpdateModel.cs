namespace Cell.Application.Api.Models.SettingView
{
    public class SettingViewUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SettingViewSettingsModel Settings { get; set; }
    }
}