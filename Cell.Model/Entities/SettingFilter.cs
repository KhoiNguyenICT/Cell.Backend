using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SettingFilterEntity
{
    [Table("T_SETTING_FILTER")]
    public class SettingFilter : Entity
    {
        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; set; }

        [Column("TABLE_ID_TEXT")]
        public string TableIdText { get; set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; set; }
    }
}