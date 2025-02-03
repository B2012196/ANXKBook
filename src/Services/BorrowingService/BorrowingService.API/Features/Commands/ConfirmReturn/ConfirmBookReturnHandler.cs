namespace BorrowingService.API.Features.Commands.ConfirmReturn
{
    public record ConfirmBookReturnCommand(Guid RecordId, List<ReturnedBook> Books) : ICommand<ConfirmBookReturnResult>;
    public record ConfirmBookReturnResult(bool IsSuccess);
    public class ConfirmBookReturnCommandValidator : AbstractValidator<ConfirmBookReturnCommand>
    {
        public ConfirmBookReturnCommandValidator()
        {
            RuleFor(x => x.RecordId).NotEmpty().WithMessage("RecordId is required.")
                .NotEqual(Guid.Empty).WithMessage("RecordId cannot be an empty GUID.");

            RuleFor(x => x.Books).NotEmpty().WithMessage("FullName is required.")
                .ForEach(book =>
                {
                    book.NotNull().WithMessage("Book cannot be null.");
                    book.ChildRules(b =>
                    {
                        b.RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId is required.")
                            .NotEqual(Guid.Empty).WithMessage("BookId cannot be an empty GUID.");

                        b.RuleFor(x => x.BookStatusId).NotEmpty().WithMessage("BookStatusId is required.")
                            .NotEqual(Guid.Empty).WithMessage("BookStatusId cannot be an empty GUID.");
                    });
                });
        }
    }
    public class ConfirmBookReturnHandler(IMongoDatabase mongo, IPublishEndpoint publishEndpoint) : ICommandHandler<ConfirmBookReturnCommand, ConfirmBookReturnResult>
    {
        public async Task<ConfirmBookReturnResult> Handle(ConfirmBookReturnCommand command, CancellationToken cancellationToken)
        {
            var collection = mongo.GetCollection<BorrowingRecord>("BorrowingRecords");

            var filter = Builders<BorrowingRecord>.Filter.Eq(r => r.RecordId, command.RecordId);

            var record = await collection.Find(filter).SingleOrDefaultAsync(cancellationToken);

            if (record == null)
            {
                throw new RecordNotFoundException(command.RecordId);
            }

            var update = Builders<BorrowingRecord>.Update
                .Set(r => r.Status, BorrowingStatus.Returned)
                .Set(r => r.ReturnDate, DateTime.UtcNow);

            await collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

            //send BorrowedEvent event to RabbtMQ  using Masstransit
            var eventMessage = command.Adapt<ReturnedEvent>();
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new ConfirmBookReturnResult(true);
        }
    }
}
