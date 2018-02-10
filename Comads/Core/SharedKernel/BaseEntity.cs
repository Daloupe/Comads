using System.Collections.Generic;

namespace Core.SharedKernel
{
    // This can be modified to BaseEntity<TId> to support multiple key types (e.g. Guid)
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        readonly List<BaseDomainEvent> _events = new List<BaseDomainEvent>();
        public IReadOnlyList<BaseDomainEvent> Events => _events;
    }
}