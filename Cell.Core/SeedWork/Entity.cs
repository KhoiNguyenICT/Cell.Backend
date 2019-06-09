using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using Newtonsoft.Json;

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
}