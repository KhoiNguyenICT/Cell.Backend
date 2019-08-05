using System;

namespace Cell.Model.Models.SettingFeature
{
    public class SettingFeatureUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public SettingFeatureSettingsModel Settings { get; set; }
    }
}