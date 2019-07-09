using Cell.Core.SeedWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Domain.Aggregates.SettingAdvancedAggregate
{
    [Table("T_SETTING_ADVANCED")]
    public class SettingAdvanced : TreeEntity, IAggregateRoot
    {
        [Column("SETTING_VALUE")]
        public string SettingValue { get; private set; }

        [NotMapped]
        public List<SettingAdvanced> Children { get; set; }

        public SettingAdvanced()
        {
        }

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