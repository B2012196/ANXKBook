namespace BuildingBlocks.Messaging.Events
{
    public record ReturnedEvent : IntegrationEvent
    {
        public Guid RecordId { get; set; }
        public List<ReturnedBook> Books { get; set; }
    }

    public record ReturnedBook
    {
        public Guid BookId { get; set; }
        public Guid BookStatusId { get; set; }
    }
}
