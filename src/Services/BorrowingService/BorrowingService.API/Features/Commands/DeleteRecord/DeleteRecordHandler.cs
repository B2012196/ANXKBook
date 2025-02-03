namespace BorrowingService.API.Features.Commands.DeleteRecord
{
    public record DeleteRecordCommand(Guid RecordId) : ICommand<DeleteRecordResult>;
    public record DeleteRecordResult(bool IsSuccess);
    public class DeleteRecordCommandValidator : AbstractValidator<DeleteRecordCommand>
    {
        public DeleteRecordCommandValidator()
        {
            RuleFor(x => x.RecordId).NotEmpty().WithMessage("RecordId is required.")
                .NotEqual(Guid.Empty).WithMessage("RecordId cannot be an empty GUID.");
        }
    }
    public class DeleteRecordHandler(IMongoDatabase mongo) : ICommandHandler<DeleteRecordCommand, DeleteRecordResult>
    {
        public async Task<DeleteRecordResult> Handle(DeleteRecordCommand command, CancellationToken cancellationToken)
        {
            var collection = mongo.GetCollection<BorrowingRecord>("BorrowingRecords");

            var filter = Builders<BorrowingRecord>.Filter.Eq(record => record.RecordId, command.RecordId);

            var record = await collection.DeleteOneAsync(filter, cancellationToken);

            if(record.DeletedCount == 0)
            {
                throw new RecordNotFoundException(command.RecordId);
            }

            return new DeleteRecordResult(true);

        }
    }
}
