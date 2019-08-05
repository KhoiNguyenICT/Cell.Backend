using System;
using Cell.Common.SeedWork;

namespace Cell.Model.Models.SettingReport
{
    public class SettingReportModel : BaseModel
    {
        public Guid TableId { get; set; }

        public string TableIdText { get; set; }

        public string TableName { get; set; }
        public SettingReportSettingsModel Settings { get; set; }
    }
}