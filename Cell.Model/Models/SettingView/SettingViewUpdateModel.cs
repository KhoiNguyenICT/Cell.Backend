using System;

namespace Cell.Model.Models.SettingView
{
    public class SettingViewUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SettingViewSettingsModel Settings { get; set; }
    }
}