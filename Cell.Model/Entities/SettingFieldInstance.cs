using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Model.Entities.SettingFieldInstanceEntity
{
    [Table("T_SETTING_FIELD_INSTANCE")]
    public class SettingFieldInstance : Entity
    {
        [Column("CAPTION")]
        [StringLength(200)]
        public string Caption { get; set; }

        [Column("CONTAINER_TYPE")]
        [StringLength(50)]
        public string ContainerType { get; set; }

        [Column("DATA_TYPE")]
        [StringLength(50)]
        public string DataType { get; set; }

        [Column("FIELD_ID")]
        public Guid FieldId { get; set; }

        [Column("ORDINAL_POSITION")]
        public int OrdinalPosition { get; set; }

        [Column("PARENT")]
        public Guid Parent { get; set; }

        [Column("PARENT_TEXT")]
        public string ParentText { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("STORAGE_TYPE")]
        [StringLength(50)]
        public string StorageType { get; set; }
    }
}