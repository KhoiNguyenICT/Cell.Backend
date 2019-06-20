using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SettingFieldAggregate
{
    [Table("T_SETTING_FIELD")]
    public class SettingField : Entity, IAggregateRoot
    {
        [Column("ALLOW_FILTER")]
        public int AllowFilter { get; private set; }

        [Column("ALLOW_SUMMARY")]
        public int AllowSummary { get; private set; }

        [Column("CAPTION")]
        [StringLength(200)]
        public string Caption { get; private set; }

        [Column("DATA_TYPE")]
        [StringLength(50)]
        public string DataType { get; private set; }

        [Column("ORDINAL_POSITION")]
        public int OrdinalPosition { get; private set; }

        [Column("PLACE_HOLDER")]
        [StringLength(200)]
        public string PlaceHolder { get; private set; }

        [Column("SETTINGS")]
        public string Settings { get; private set; }

        [Column("STORAGE_TYPE")]
        [StringLength(50)]
        public string StorageType { get; private set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; private set; }

        [Column("TABLE_NAME")]
        [StringLength(200)]
        public string TableName { get; private set; }

        public SettingField(
            string name,
            string description,
            string code,
            int allowFilter, 
            int allowSummary, 
            string caption, 
            string dataType, 
            int ordinalPosition, 
            string placeHolder,
            string settings,
            string storageType,
            Guid tableId,
            string tableName)
        {
            Name = name;
            Description = description;
            Code = code;
            AllowFilter = allowFilter;
            AllowSummary = allowSummary;
            Caption = caption;
            DataType = dataType;
            OrdinalPosition = ordinalPosition;
            PlaceHolder = placeHolder;
            Settings = settings;
            StorageType = storageType;
            TableId = tableId;
            TableName = tableName;
        }

        public void Update(
            string name,
            string description,
            int allowFilter,
            int allowSummary,
            string caption,
            int ordinalPosition,
            string placeHolder,
            string settings)
        {
            Name = name;
            Description = description;
            AllowFilter = allowFilter;
            AllowSummary = allowSummary;
            Caption = caption;
            OrdinalPosition = ordinalPosition;
            PlaceHolder = placeHolder;
            Settings = settings;
        }
    }
}