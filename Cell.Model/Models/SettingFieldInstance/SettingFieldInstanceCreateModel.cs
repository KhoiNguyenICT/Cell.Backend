using System;

namespace Cell.Model.Models.SettingFieldInstance
{
    public class SettingFieldInstanceCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        public string ContainerType { get; set; }
        public string DataType { get; set; }
        public Guid FieldId { get; set; }
        public int OrdinalPosition { get; set; }
        public Guid Parent { get; set; }
        public string ParentText { get; set; }
        public string StorageType { get; set; }
        public SettingFieldInstanceSettingsModel Settings { get; set; }
    }
}