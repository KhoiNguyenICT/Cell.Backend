using System;
using Cell.Common.SeedWork;

namespace Cell.Model.Models.SettingForm
{
    public class SettingFormModel : BaseModel
    {
        public Guid LayoutId { get; set; }

        public SettingFormSettingsModel Settings { get; set; }

        public Guid TableId { get; set; }

        public string TableName { get; set; }
    }
}