using MediatR;

namespace BuildingBlocks.Messaging.Events
{
    public record IntegrationEvent : INotification
    {
        public Guid Id => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
