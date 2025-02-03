namespace BookService.API.Features.BookCopys.EventHandlers.Integration
{
    public class BorrowedEventHandler(IBookCopyRepository bookCopyRepository, ILogger<BorrowedEventHandler> logger) : IConsumer<BorrowedEvent>
    {
        public async Task Consume(ConsumeContext<BorrowedEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
            var eventMessage = context.Message;
            foreach (var book in eventMessage.Books)
            {
                await bookCopyRepository.UpdateBorrowedStatus(book.BookId, context.CancellationToken);
            }
        }
    }
}
