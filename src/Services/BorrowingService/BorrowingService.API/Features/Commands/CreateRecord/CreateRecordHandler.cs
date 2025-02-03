namespace BorrowingService.API.Features.Commands.CreateRecord
{
    public record CreateRecordCommand
        (string FullName, List<BorrowedBook> Books) : ICommand<CreateRecordResult>;
    public record CreateRecordResult(Guid RecordId);
    public class CreateRecordCommandValidator : AbstractValidator<CreateRecordCommand>
    {
        public CreateRecordCommandValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("FullName is required.")
                .MaximumLength(30).WithMessage("FullName cannot exceed 30 characters.");

            RuleFor(x => x.Books).NotEmpty().WithMessage("FullName is required.")
                .ForEach(book =>
                {
                    book.NotNull().WithMessage("Book cannot be null.");
                    book.ChildRules(b =>
                    {
                        b.RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId is required.")
                            .NotEqual(Guid.Empty).WithMessage("BookId cannot be an empty GUID.");

                        b.RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.")
                            .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.");
                    });
                });
        }
    }
    public class CreateRecordHandler(IMongoDatabase mongo) : ICommandHandler<CreateRecordCommand, CreateRecordResult>
    {
        public async Task<CreateRecordResult> Handle(CreateRecordCommand command, CancellationToken cancellationToken)
        {
            var collection = mongo.GetCollection<BorrowingRecord>("BorrowingRecords");

            ////b1. Kiem tra sach co available
            //foreach(var book in command.Books)
            //{
            //    var filter = Builders<BorrowingRecord>.Filter.Eq(record => record.RecordId, command.RecordId)
            //}

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
