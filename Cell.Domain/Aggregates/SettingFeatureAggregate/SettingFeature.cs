using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SettingFeatureAggregate
{
    [Table("T_SETTING_FEATURE")]
    public class SettingFeature : TreeEntity, IAggregateRoot
    {
        [Column("ICON")]
        [StringLength(50)]
        public string Icon { get; private set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [NotMapped]
        public List<SettingFeature> Children { get; set; }

        public SettingFeature() { }

        public SettingFeature(
            string name,
            string settings,
            Guid parent,
            string icon)
        {
            Name = name;
            Settings = settings;
            Parent = parent;
            Icon = icon;
        }

        public void Update(
            string name,
            string description,
            string icon,
            string settings)
        {
            Name = name;
            Description = description;
            Icon = icon;
            Settings = settings;
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