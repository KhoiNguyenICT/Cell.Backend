using Cell.Common.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SettingApiEntity
{
    [Table("T_SETTING_API")]
    public class SettingApi : Entity
    {
        [Column("LIBRARY")]
        public string Library { get; set; }

        [Column("METHOD")]
        [StringLength(200)]
        public string Method { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; set; }
    }
}