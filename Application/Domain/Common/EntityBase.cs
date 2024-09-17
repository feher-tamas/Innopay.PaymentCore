using FT.CQRS;
using FunctionExtensions.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain.Common
{
    public abstract class EntityBase : Entity
    {
        private readonly List<IEvent> _domainEvents = new List<IEvent>();
        public IReadOnlyList<IEvent> DomainEvents => _domainEvents;
        protected void RaiseDomainEvent(IEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
