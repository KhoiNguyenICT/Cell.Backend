using Cell.Core.SeedWork;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Domain.Aggregates.SettingElasticSearch
{
    [Table("T_SETTING_ELASTIC_SEARCH")]
    public class SettingElasticSearch : IAggregateRoot
    {
        [Column("ID")]
        public Guid Id { get; set; }

        [Column("OBJECT_ID")]
        public Guid ObjectId { get; set; }

        [Column("TABLE_NAME")]
        public string TableName { get; set; }

        [Column("TABLE_ID")]
        public Guid TableId { get; set; }

        [Column("DATA")]
        public string Data { get; set; }
    }
}