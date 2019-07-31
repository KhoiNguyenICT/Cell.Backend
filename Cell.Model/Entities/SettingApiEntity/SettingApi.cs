using System;
using Cell.Common.SeedWork;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SettingApiEntity
{
    [Table("T_SETTING_API")]
    public class SettingApi : Entity
    {
        [Column("LIBRARY")]
        public string Library { get; set; }

        [Column("METHOD")]
        public string Method { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; set; }

        [Column("TABLE_NAME")]
        public string TableName { get; set; }
    }
}