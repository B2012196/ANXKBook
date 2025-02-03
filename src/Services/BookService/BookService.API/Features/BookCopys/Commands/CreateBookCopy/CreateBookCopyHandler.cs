namespace BookService.API.Features.BookCopys.Commands.CreateBookCopy
{
    public record CreateBookCopyCommand(Guid BookId) : ICommand<CreateBookCopyResult>;
    public record CreateBookCopyResult(Guid BookCopyId);

    public class CreateBookCopyCommandValidator : AbstractValidator<CreateBookCopyCommand>
    {
        public CreateBookCopyCommandValidator()
        {
            RuleFor(x => x.BookId).NotEmpty().WithMessage("BookId is required.")
                .NotEqual(Guid.Empty).WithMessage("BookId cannot be an empty GUID.");
        }
    }
    public class CreateBookCopyHandler
        (IPublishEndpoint publishEndpoint, ApplicationDbContext context, IStatusRepository statusRepository) 
            : ICommandHandler<CreateBookCopyCommand, CreateBookCopyResult>
    {
        public async Task<CreateBookCopyResult> Handle(CreateBookCopyCommand command, CancellationToken cancellationToken)
        {
            //lay status 
            var status = await statusRepository.GetStatusByNameAsync("Available", cancellationToken)
                ?? throw new StatusNotFoundException(Guid.Empty);

            var copy = new BookCopy
            {
                BookCopyId = Guid.NewGuid(),
                BookId = command.BookId,
                BookStatusId = status.StatusId
            };

            context.BookCopys.Add(copy);
            await context.SaveChangesAsync(cancellationToken);

            var eventMessage = command.Adapt<ChangeStatusEvent>();
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new CreateBookCopyResult(copy.BookCopyId);

        }
    }
}
