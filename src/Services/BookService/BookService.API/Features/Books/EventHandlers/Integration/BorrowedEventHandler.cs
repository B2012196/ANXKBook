namespace BookService.API.Features.Books.EventHandlers.Integration
{
    public class BorrowedEventHandler(IBookRepository bookRepository,ILogger<BorrowedEventHandler> logger) : IConsumer<BorrowedEvent>
    {
        public async Task Consume(ConsumeContext<BorrowedEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
            var eventMessage = context.Message; 
            foreach(var book in eventMessage.Books)
            {
                await bookRepository.UpdateBorrowedStatus(book.BookId, context.CancellationToken);
            }
        }
    }
}
