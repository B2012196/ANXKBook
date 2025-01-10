namespace BookService.API.Features.Books.EventHandlers.Integration
{
    public class ReturnedEventHandler(IBookRepository bookRepository, ILogger<ReturnedEventHandler> logger) : IConsumer<ReturnedEvent>
    {
        public async Task Consume(ConsumeContext<ReturnedEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
            var eventMessage = context.Message;

            foreach(var book in eventMessage.Books)
            {
                await bookRepository.UpdateReturnedStatus(book.BookId, book.BookStatusId, context.CancellationToken);
            }
        }
    }
}
