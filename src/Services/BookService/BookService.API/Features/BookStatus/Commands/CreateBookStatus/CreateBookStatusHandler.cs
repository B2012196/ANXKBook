namespace BookService.API.Features.BookStatus.Commands.CreateBookStatus
{
    public record CreateBookStatusCommand(string StatusName) : ICommand<CreateBookStatusResult>;
    public record CreateBookStatusResult(Guid StatusId);
    public class CreateBookStatusCommandValidator : AbstractValidator<CreateBookStatusCommand>
    {
        public CreateBookStatusCommandValidator()
        {
            RuleFor(x => x.StatusName).NotEmpty().WithMessage("StatusName is required.")
                .MaximumLength(20).WithMessage("StatusName cannot exceed 20 characters.");
        }
    }
    public class CreateBookStatusHandler(ApplicationDbContext context)
        : ICommandHandler<CreateBookStatusCommand, CreateBookStatusResult>
    {
        public async Task<CreateBookStatusResult> Handle(CreateBookStatusCommand command, CancellationToken cancellationToken)
        {
            var status = new Status
            {
                StatusId = Guid.NewGuid(),
                StatusName = command.StatusName,
            };

            context.BookStatus.Add(status);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateBookStatusResult(status.StatusId);

        }
    }
}
