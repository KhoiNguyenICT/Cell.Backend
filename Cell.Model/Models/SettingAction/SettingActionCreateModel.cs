using System;

namespace Cell.Model.Models.SettingAction
{
    public class SettingActionCreateModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContainerType { get; set; }
        public string TableName { get; set; }
        public Guid TableId { get; set; }
        public SettingActionSettingsModel Settings { get; set; }
    }
}