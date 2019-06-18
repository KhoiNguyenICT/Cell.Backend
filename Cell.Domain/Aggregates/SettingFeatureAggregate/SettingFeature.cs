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
        public string Icon { get; set; }

        [Column("INDEX_LEFT")]
        public int IndexLeft { get; set; }

        [Column("INDEX_RIGHT")]
        public int IndexRight { get; set; }

        [Column("IS_LEAF")]
        public int IsLeaf { get; set; }

        [Column("NODE_LEVEL")]
        public int NodeLevel { get; set; }

        [Column("PARENT")]
        public Guid Parent { get; set; }

        [Column("PATH_CODE")]
        [StringLength(1000)]
        public string PathCode { get; set; }

        [Column("PATH_ID")]
        [StringLength(1000)]
        public string PathId { get; set; }

        [Column("SETTINGS")]
        public string Settings { get; set; }

        [NotMapped]
        public List<SettingFeature> Children { get; set; }

        public SettingFeature() { }

        public SettingFeature(
            string name,
            string description,
            string icon)
        {
            Name = name;
            Description = description;
            Icon = icon;
        }

        public void Update(
            string name,
            string description,
            string icon)
        {
            Name = name;
            Description = description;
            Icon = icon;
        }

        public void UpdateParent(
            Guid parent)
        {
            Parent = parent;
        }
    }
}