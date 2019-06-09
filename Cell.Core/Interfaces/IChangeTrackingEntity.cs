using MediatR;
using System.Collections.Generic;

namespace Cell.Core.Interfaces
{
    public interface IChangeTrackingEntity
    {
        IReadOnlyCollection<INotification> DomainEvents { get; }

        void ClearDomainEvents();

        void AddDomainEvent(INotification eventItem);
    }
}