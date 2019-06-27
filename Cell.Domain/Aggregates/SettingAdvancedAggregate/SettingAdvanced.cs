using Cell.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Domain.Aggregates.SettingAdvancedAggregate
{
    [Table("T_SETTING_ADVANCED")]
    public class SettingAdvanced : Entity, IAggregateRoot
    {
        [Column("INDEX_LEFT")]
        public int IndexLeft { get; private set; }

        [Column("INDEX_RIGHT")]
        public int IndexRight { get; private set; }

        [Column("IS_LEAF")]
        public int IsLeaf { get; private set; }

        [Column("NODE_LEVEL")]
        public int NodeLevel { get; private set; }

        [Column("PARENT")]
        public Guid? Parent { get; private set; }

        [Column("PATH_CODE")]
        [StringLength(1000)]
        public string PathCode { get; private set; }

        [Column("PATH_ID")]
        [StringLength(1000)]
        public string PathId { get; private set; }

        [Column("SETTING_VALUE")]
        public string SettingValue { get; private set; }

        [NotMapped]
        public List<SettingAdvanced> Children { get; set; }

        public SettingAdvanced() { }

        public SettingAdvanced(
            string name,
            Guid parent)
        {
            Name = name;
            Parent = parent;
        }

        public void Update(
            string name,
            string description)
        {
            Name = name;
            Description = description;
        }

        public void Rename(string name)
        {
            Name = name;
        }

        public void UpdateParent(
            Guid parent)
        {
            Parent = parent;
        }
    }
}