using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cell.Common.SeedWork;

namespace Cell.Model.Entities.SettingViewEntity
{
    [Table("T_SETTING_VIEW")]
    public class SettingView : Entity
    {
        [Column("TABLE_ID")]
        public Guid TableId { get; set; }

        [StringLength(200)]
        [Column("TABLE_NAME")]
        public string TableName { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }
    }
}