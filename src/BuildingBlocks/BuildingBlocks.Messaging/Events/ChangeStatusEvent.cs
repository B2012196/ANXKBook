namespace BuildingBlocks.Messaging.Events
{
    public record ChangeStatusEvent : IntegrationEvent
    {
        public Guid BookId { get; set; }
        public Guid BookStatusId { get; set; }
    }
}
