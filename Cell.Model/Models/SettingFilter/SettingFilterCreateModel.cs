using System;

namespace Cell.Model.Models.SettingFilter
{
    public class SettingFilterCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Settings { get; set; }
        public Guid TableId { get; set; }
        public string TableName { get; set; }
    }
}