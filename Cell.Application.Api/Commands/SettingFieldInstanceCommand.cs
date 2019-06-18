using System;
using System.Collections.Generic;

namespace Cell.Application.Api.Commands
{
    public class SettingFieldInstanceCommand : Command
    {
        public string Caption { get; set; }

        public string ContainerType { get; set; }

        public string DataType { get; set; }

        public Guid FieldId { get; set; }

        public int OrdinalPosition { get; set; }

        public Guid Parent { get; set; }

        public SettingFieldInstanceSettings Settings { get; set; }

        public string StorageType { get; set; }
    }

    public class SettingFieldInstanceSettings
    {
        public SettingFieldInstanceSettingTable Table { get; set; }
        public SettingFieldInstanceSettingView View { get; set; }
    }

    public class SettingFieldInstanceSettingTable
    {
        public string Font { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public string Description { get; set; }
        public string BackgroundColor { get; set; }
        public string HighlightColor { get; set; }
        public string FontAlign { get; set; }
        public int FontWidth { get; set; }
        public List<string> FontStyle { get; set; }
    }

    public class SettingFieldInstanceSettingView
    {
        public string Font { get; set; }
        public string FontColor { get; set; }
        public int FontSize { get; set; }
        public string Description { get; set; }
        public string BackgroundColor { get; set; }
        public string HighlightColor { get; set; }
        public string FontAlign { get; set; }
        public int FontWidth { get; set; }
        public List<string> FontStyle { get; set; }
    }
}