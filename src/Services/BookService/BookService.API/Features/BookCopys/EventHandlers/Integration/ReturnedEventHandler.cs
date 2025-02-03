namespace BookService.API.Features.BookCopys.EventHandlers.Integration
{
    public class ReturnedEventHandler(IBookCopyRepository bookCopyRepository, ILogger<ReturnedEventHandler> logger) : IConsumer<ReturnedEvent>
    {
        public async Task Consume(ConsumeContext<ReturnedEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
            var eventMessage = context.Message;

            foreach (var book in eventMessage.Books)
            {
                await bookCopyRepository.UpdateReturnedStatus(book.BookId, book.BookStatusId, context.CancellationToken);
            }
        }
    }
}
