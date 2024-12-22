namespace BookService.API.Features.BookStatus.Commands.UpdateBookStatus
{
    public record UpdateStatusCommand(Guid StatusId, string StatusName) : ICommand<UpdateStatusResult>;
    public record UpdateStatusResult(bool IsSuccess);
    public class UpdateStatusHandler(ApplicationDbContext context) : ICommandHandler<UpdateStatusCommand, UpdateStatusResult>
    {
        public async Task<UpdateStatusResult> Handle(UpdateStatusCommand command, CancellationToken cancellationToken)
        {
            var status = await context.BookStatus.SingleOrDefaultAsync(s => s.StatusId == command.StatusId ,cancellationToken);

            if (status == null)
            {
                throw new StatusNotFoundException(command.StatusId);
            }

            status.StatusName = command.StatusName;

            context.BookStatus.Update(status);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateStatusResult(true);
        }
    }
}
