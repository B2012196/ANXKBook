
namespace BookService.API.Features.BookStatus.Commands.DeleteBookStatus
{
    public record DeleteStatusCommand(Guid StatusId) : ICommand<DeleteStatusResult>;
    public record DeleteStatusResult(bool IsSuccess);
    public class DeleteStatusHandler(ApplicationDbContext context) : ICommandHandler<DeleteStatusCommand, DeleteStatusResult>
    {
        public async Task<DeleteStatusResult> Handle(DeleteStatusCommand command, CancellationToken cancellationToken)
        {
            var status = await context.BookStatus.SingleOrDefaultAsync(s => s.StatusId == command.StatusId, cancellationToken);  
            if (status == null)
            {
                throw new StatusNotFoundException(command.StatusId);
            }

            context.BookStatus.Remove(status);

            await context.SaveChangesAsync(cancellationToken);

            return new DeleteStatusResult(true);
        }
    }
}
