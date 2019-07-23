using System;

namespace Cell.Application.Api.Models.SettingReport
{
    public class SettingReportCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Settings { get; set; }
        public Guid TableId { get; set; }
        public string TableName { get; set; }
    }
}