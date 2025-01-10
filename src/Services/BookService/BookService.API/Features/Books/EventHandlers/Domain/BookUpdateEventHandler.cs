namespace BookService.API.Features.Books.EventHandlers.Domain
{
    public class BookUpdateEventHandler(ILogger<BookUpdateEventHandler> logger) : INotificationHandler<BorrowedEvent> 
    { 
        public Task Handle(BorrowedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
