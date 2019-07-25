using System;

namespace Cell.Model.Models.SettingActionInstance
{
    public class SettingActionInstanceUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SettingActionInstanceSettingsModel Settings { get; set; }
    }
}