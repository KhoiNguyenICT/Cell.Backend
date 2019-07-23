using Cell.Common.SeedWork;
using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Models.SettingView
{
    public class SettingViewModel : BaseModel
    {
        public Guid TableId { get; set; }
        public string TableName { get; set; }
        public string TableIdText { get; set; }
        public SettingViewSettingsModel Settings { get; set; }
    }

    public class SettingViewSettingsModel
    {
        public ActionViewSettingsModel Action { get; set; }
        public FieldViewSettingsModel Field { get; set; }
    }

    public class ActionViewSettingsModel
    {
        public string Positions { get; set; }
        public List<ActionViewItemModel> Items { get; set; }
    }

    public class FieldViewSettingsModel
    {
        public string Positions { get; set; }
        public List<FieldViewItemModel> Items { get; set; }
    }

    public class ActionViewItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class FieldViewItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}