using System;
using Cell.Common.SeedWork;

namespace Cell.Model.Models.SettingFilter
{
    public class SettingFilterModel : BaseModel
    {
        public string Settings { get; set; }

        public Guid TableId { get; set; }

        public string TableIdText { get; set; }

        public string TableName { get; set; }
    }
}