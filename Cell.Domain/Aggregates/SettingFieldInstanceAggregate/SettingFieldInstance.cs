using Cell.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace Cell.Domain.Aggregates.SettingFieldInstanceAggregate
{
    [Table("T_SETTING_FIELD_INSTANCE")]
    public class SettingFieldInstance : Entity, IAggregateRoot
    {
        [Column("CAPTION")]
        [StringLength(200)]
        public string Caption { get; private set; }

        [Column("CONTAINER_TYPE")]
        [StringLength(50)]
        public string ContainerType { get; private set; }

        [Column("DATA_TYPE")]
        [StringLength(50)]
        public string DataType { get; private set; }

        [Column("FIELD_ID")]
        public Guid FieldId { get; private set; }

        [Column("ORDINAL_POSITION")]
        public int OrdinalPosition { get; private set; }

        [Column("PARENT")]
        public Guid Parent { get; private set; }

        [Column("PARENT_TEXT")]
        public string ParentText { get; private set; }

        [Column("SETTINGS")]
        public string Settings { get; private set; }

        [Column("STORAGE_TYPE")]
        [StringLength(50)]
        public string StorageType { get; private set; }

        public SettingFieldInstance() { }

        public SettingFieldInstance(
            string name,
            string description,
            string caption,
            string containerType,
            string dataType,
            Guid fieldId,
            int ordinalPosition,
            Guid parent,
            string parentText,
            string settings,
            string storageType)
        {
            Name = name;
            Description = description;
            Caption = caption;
            ContainerType = containerType;
            DataType = dataType;
            FieldId = fieldId;
            OrdinalPosition = ordinalPosition;
            Parent = parent;
            ParentText = parentText;
            Settings = settings;
            StorageType = storageType;
        }

        public void Update(
            string settings)
        {
            Settings = settings;
        }
    }
}