using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cell.Common.SeedWork;

namespace Cell.Model.Entities.SettingFieldEntity
{
    [Table("T_SETTING_FIELD")]
    public class SettingField : Entity
    {
        [Column("ALLOW_FILTER")]
        public bool AllowFilter { get; set; }

        [Column("ALLOW_SUMMARY")]
        public bool AllowSummary { get; set; }

        [Column("CAPTION")]
        [StringLength(200)]
        public string Caption { get; set; }

        [Column("DATA_TYPE")]
        [StringLength(50)]
        public string DataType { get; set; }

        [Column("ORDINAL_POSITION")]
        public int OrdinalPosition { get; set; }

        [Column("PLACE_HOLDER")]
        [StringLength(200)]
        public string PlaceHolder { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("STORAGE_TYPE")]
        [StringLength(50)]
        public string StorageType { get; set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; set; }
    }
}