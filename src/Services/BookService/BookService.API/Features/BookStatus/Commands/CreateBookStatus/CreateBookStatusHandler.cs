namespace BookService.API.Features.BookStatus.Commands.CreateBookStatus
{
    public record CreateBookStatusCommand(string StatusName) : ICommand<CreateBookStatusResult>;
    public record CreateBookStatusResult(Guid StatusId);
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
