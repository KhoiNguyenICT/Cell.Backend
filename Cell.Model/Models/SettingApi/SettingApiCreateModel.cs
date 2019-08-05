using System;

namespace Cell.Model.Models.SettingApi
{
    public class SettingApiCreateModel
    {
        public string Library { get; set; }

        public string Method { get; set; }

        public Guid TableId { get; set; }

        public string TableName { get; set; }
        public string Settings { get; set; }
    }
}