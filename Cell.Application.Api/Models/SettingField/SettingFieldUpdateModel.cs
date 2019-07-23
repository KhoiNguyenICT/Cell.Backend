namespace Cell.Application.Api.Models.SettingField
{
    public class SettingFieldUpdateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int AllowFilter { get; set; }
        public int AllowSummary { get; set; }
        public string Caption { get; set; }
        public int OrdinalPosition { get; set; }
        public string PlaceHolder { get; set; }
        public SettingFieldSettingsModel Settings { get; set; }
    }
}