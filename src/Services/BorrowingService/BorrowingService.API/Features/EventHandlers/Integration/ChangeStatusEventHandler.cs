
namespace BorrowingService.API.Features.EventHandlers.Integration
{
    public class ChangeStatusEventHandler(IMongoDatabase mongo, ILogger<ChangeStatusEventHandler> logger)
        : IConsumer<ChangeStatusEvent>
    {
        public async Task Consume(ConsumeContext<ChangeStatusEvent> context)
        {
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
            var eventMessage = context.Message;

            var collection = mongo.GetCollection<Book>("Books");

            var book = new Book
            {
                BookId = eventMessage.BookId,
                BookStatusId = eventMessage.BookStatusId,
            };

            await collection.InsertOneAsync(book, cancellationToken: context.CancellationToken);
        }
    }
}
