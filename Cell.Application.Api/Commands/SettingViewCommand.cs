using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Commands
{
    public class SettingViewCommand: Command
    {
        public Guid TableId { get; set; }
        public string TableName { get; set; }
        public string TableIdText { get; set; }
        public SettingViewSettingsCommand Settings { get; set; }
    }

    public class SettingViewSettingsCommand
    {
        public ActionViewSettingsCommand Action { get; set; }
        public FieldViewSettings Field { get; set; }
    }

    public class ActionViewSettingsCommand
    {
        public string Positions { get; set; }
        public List<ActionViewItem> Items { get; set; }
    }

    public class FieldViewSettings
    {
        public string Positions { get; set; }
        public List<FieldViewItem> Items { get; set; }
    }

    public class ActionViewItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class FieldViewItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}