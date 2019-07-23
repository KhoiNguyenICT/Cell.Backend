using System;

namespace Cell.Application.Api.Models.SettingFeature
{
    public class SettingFeatureCreateModel
    {
        public string Name { get; set; }
        public Guid Parent { get; set; }
        public string Icon { get; set; }
        public SettingFeatureSettingsModel Settings { get; set; }
    }
}