using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Common.SeedWork
{
    public abstract class Entity : IEntity
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; set; }

        [Column("CODE")]
        public string Code { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("DATA", TypeName = "xml")]
        public string Data { get; set; }

        [Column("CREATED")]
        public DateTimeOffset Created { get; set; }

        [Column("CREATED_BY")]
        public Guid CreatedBy { get; set; }

        [Column("MODIFIED")]
        public DateTimeOffset Modified { get; set; }

        [Column("MODIFIED_BY")]
        public Guid ModifiedBy { get; set; }

        [Column("VERSION")]
        public int Version { get; set; }
    }

    public abstract class TreeEntity : Entity
    {
        [Column("INDEX_LEFT")]
        public int IndexLeft { get; set; }

        [Column("INDEX_RIGHT")]
        public int IndexRight { get; set; }

        [Column("IS_LEAF")]
        public int IsLeaf { get; set; }

        [Column("NODE_LEVEL")]
        public int NodeLevel { get; set; }

        [Column("PARENT")]
        public Guid? Parent { get; set; }

        [Column("PATH_CODE")]
        [StringLength(1000)]
        public string PathCode { get; set; }

        [Column("PATH_ID")]
        [StringLength(1000)]
        public string PathId { get; set; }
    }
}