using System.Collections.Generic;

namespace Cell.Application.Api.Models.SettingFieldInstance
{
    public class SettingFieldInstanceSettingsModel
    {
        public SettingFieldInstanceSettingTableModel Table { get; set; }
        public SettingFieldInstanceSettingViewModel View { get; set; }
    }

    public class SettingFieldInstanceSettingTableModel
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

    public class SettingFieldInstanceSettingViewModel
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