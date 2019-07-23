using System;
using System.Collections.Generic;
using Cell.Common.SeedWork;

namespace Cell.Application.Api.Models.SettingAction
{
    public class SettingActionModel : BaseModel
    {
        public string ContainerType { get; set; }

        public SettingActionSettingsModel Settings { get; set; }

        public Guid TableId { get; set; }

        public string TableName { get; set; }
    }
}