using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cell.Core.SeedWork;

namespace Cell.Domain.Aggregates.SecurityGroupAggregate
{
    [Table("T_SECURITY_GROUP")]
    public class SecurityGroup : TreeEntity, IAggregateRoot
    {
        [Column("SETTINGS")]
        public string Settings { get; set; }

        [Column("STATUS")]
        public Guid Status { get; private set; }

        [NotMapped]
        public List<SecurityGroup> Children { get; set; }

        public SecurityGroup() { }

        public SecurityGroup(
            string code,
            string name,
            string description,
            Guid parent,
            Guid status)
        {
            Code = code;
            Name = name;
            Description = description;
            Parent = parent;
            Status = status;
        }

        public void Rename(string name)
        {
            Name = name;
        }

        public void Update(
            string name,
            string description,
            string settings)
        {
            Name = name;
            Description = description;
            Settings = settings;
        }
    }
}