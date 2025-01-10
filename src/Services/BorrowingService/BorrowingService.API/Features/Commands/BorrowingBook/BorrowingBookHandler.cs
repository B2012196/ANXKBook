﻿namespace BorrowingService.API.Features.Commands.BorrowingBook
{
    public record BorrowingBookCommand(Guid RecordId, List<BorrowedBook> Books) : ICommand<BorrowingBookResult>;
    public record BorrowingBookResult(bool IsSuccess);
    public class BorrowingBookHandler(IMongoDatabase mongo, IPublishEndpoint publishEndpoint) : ICommandHandler<BorrowingBookCommand, BorrowingBookResult>
    {
        public async Task<BorrowingBookResult> Handle(BorrowingBookCommand command, CancellationToken cancellationToken)
        {
            var collection = mongo.GetCollection<BorrowingRecord>("BorrowingRecords");

            var filter = Builders<BorrowingRecord>.Filter.Eq(r => r.RecordId, command.RecordId);

            var record = await collection.Find(filter).SingleOrDefaultAsync(cancellationToken);

            if(record == null)
            {
                throw new RecordNotFoundException(command.RecordId);
            }

            //update data
            var update = Builders<BorrowingRecord>.Update
                .Set(r => r.Books, command.Books)
                .Set(r => r.Status, BorrowingStatus.Borrowed)
                .Set(r => r.BorrowDate, DateTime.UtcNow);

            await collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

            //send BorrowedEvent event to RabbtMQ  using Masstransit
            var eventMessage = command.Adapt<BorrowedEvent>();
            await publishEndpoint.Publish(eventMessage, cancellationToken);


            return new BorrowingBookResult(true);

        }
    }
}