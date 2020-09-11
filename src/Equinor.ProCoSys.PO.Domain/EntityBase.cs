﻿using System;
using System.Collections.Generic;
using MediatR;

namespace Equinor.ProCoSys.PO.Domain
{
    /// <summary>
    /// Base class for all entities
    /// </summary>
    public abstract class EntityBase
    {
        private List<INotification> _domainEvents;

        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly() ?? (_domainEvents = new List<INotification>()).AsReadOnly();

        public virtual int Id { get; protected set; }

        public readonly byte[] RowVersion = new byte[8];

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public virtual void SetRowVersion(string value)
        {
            var newRowVersion = Convert.FromBase64String(value);
            for (var index = 0; index < newRowVersion.Length; index++)
            {
                RowVersion[index] = newRowVersion[index];
            }
        }

        public void RemoveDomainEvent(INotification eventItem) => _domainEvents?.Remove(eventItem);

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
