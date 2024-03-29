﻿using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cell.Core.SeedWork
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            var now = DateTimeOffset.Now;
            Id = Guid.NewGuid();
            Created = now;
            Modified = now;
            Version = 0;
        }

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

        [NotMapped]
        [JsonIgnore]
        private List<INotification> _domainEvents = new List<INotification>();

        [NotMapped]
        [JsonIgnore]
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
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