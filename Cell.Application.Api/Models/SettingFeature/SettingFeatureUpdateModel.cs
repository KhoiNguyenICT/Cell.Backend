namespace Cell.Application.Api.Models.SettingFeature
{
    public class SettingFeatureUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public SettingFeatureSettingsModel Settings { get; set; }
    }
}