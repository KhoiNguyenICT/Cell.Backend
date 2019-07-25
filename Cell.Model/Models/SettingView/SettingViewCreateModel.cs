using System;

namespace Cell.Model.Models.SettingView
{
    public class SettingViewCreateModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid TableId { get; set; }
        public string TableName { get; set; }
        public SettingViewSettingsModel Settings { get; set; }
    }
}