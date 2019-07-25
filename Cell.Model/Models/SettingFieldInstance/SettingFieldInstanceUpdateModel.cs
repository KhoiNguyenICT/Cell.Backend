using System;

namespace Cell.Model.Models.SettingFieldInstance
{
    public class SettingFieldInstanceUpdateModel
    {
        public Guid Id { get; set; }
        public SettingFieldInstanceSettingsModel Settings { get; set; }
    }
}