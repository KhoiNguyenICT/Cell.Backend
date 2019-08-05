using System;

namespace Cell.Model.Models.SettingField
{
    public class SettingFieldUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllowFilter { get; set; }
        public bool AllowSummary { get; set; }
        public string Caption { get; set; }
        public int OrdinalPosition { get; set; }
        public string PlaceHolder { get; set; }
        public SettingFieldSettingsModel Settings { get; set; }
    }
}