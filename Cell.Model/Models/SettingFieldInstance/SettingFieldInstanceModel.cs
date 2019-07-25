using System;
using Cell.Common.SeedWork;

namespace Cell.Model.Models.SettingFieldInstance
{
    public class SettingFieldInstanceModel : BaseModel
    {
        public string Caption { get; set; }

        public string ContainerType { get; set; }

        public string DataType { get; set; }

        public Guid FieldId { get; set; }

        public int OrdinalPosition { get; set; }

        public Guid Parent { get; set; }

        public string ParentText { get; set; }

        public SettingFieldInstanceSettingsModel Settings { get; set; }

        public string StorageType { get; set; }
    }
}