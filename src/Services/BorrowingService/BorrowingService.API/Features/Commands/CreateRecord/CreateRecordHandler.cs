namespace BorrowingService.API.Features.Commands.CreateRecord
{
    public record CreateRecordCommand
        (string FullName, List<BorrowedBook> Books) : ICommand<CreateRecordResult>;
    public record CreateRecordResult(Guid RecordId);
    public class CreateRecordHandler(IMongoDatabase mongo) : ICommandHandler<CreateRecordCommand, CreateRecordResult>
    {
        public async Task<CreateRecordResult> Handle(CreateRecordCommand command, CancellationToken cancellationToken)
        {
            var collection = mongo.GetCollection<BorrowingRecord>("BorrowingRecords");

            var record = new BorrowingRecord
            {
                RecordId = Guid.NewGuid(),
                FullName = command.FullName,
                Books = command.Books,
                Status = BorrowingStatus.Pending,
            };

            await collection.InsertOneAsync(record, cancellationToken: cancellationToken);

            return new CreateRecordResult(record.RecordId);
        }
    }
}
