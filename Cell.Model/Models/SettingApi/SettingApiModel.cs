using System;
using System.Collections.Generic;
using Cell.Common.SeedWork;

namespace Cell.Model.Models.SettingApi
{
    public class SettingApiModel : BaseModel
    {
        public string Library { get; set; }

        public string Method { get; set; }

        public List<SettingApiSettingsModel> Settings { get; set; }

        public Guid TableId { get; set; }

        public string TableName { get; set; }
    }

    public class SettingApiSettingsModel
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string PlaceHolder { get; set; }
    }
}