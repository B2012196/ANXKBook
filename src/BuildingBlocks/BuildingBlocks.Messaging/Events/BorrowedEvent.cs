namespace BuildingBlocks.Messaging.Events
{
    public record BorrowedEvent : IntegrationEvent
    {
        public Guid RecordId { get; set; }
        public List<BorrowedBookEvent> Books { get; set; }   
    }

    public record BorrowedBookEvent
    {
        public Guid BookId { get; set; } 
        public string Title { get; set; } 
    }
}
