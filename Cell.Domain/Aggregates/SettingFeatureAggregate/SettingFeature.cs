using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SettingFeatureAggregate
{
    [Table("T_SETTING_FEATURE")]
    public class SettingFeature : Entity, IAggregateRoot
    {
        [Column("ICON")]
        [StringLength(50)]
        public string Icon { get; private set; }

        [Column("INDEX_LEFT")]
        public int IndexLeft { get; private set; }

        [Column("INDEX_RIGHT")]
        public int IndexRight { get; private set; }

        [Column("IS_LEAF")]
        public int IsLeaf { get; private set; }

        [Column("NODE_LEVEL")]
        public int NodeLevel { get; private set; }

        [Column("PARENT")]
        public Guid Parent { get; set; }

        [Column("PATH_CODE")]
        [StringLength(1000)]
        public string PathCode { get; private set; }

        [Column("PATH_ID")]
        [StringLength(1000)]
        public string PathId { get; private set; }

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