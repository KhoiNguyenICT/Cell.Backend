using System;

namespace Cell.Model.Models.SettingAction
{
    public class SettingActionUpdateModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContainerType { get; set; }
        public SettingActionSettingsModel Settings { get; set; }
    }
}