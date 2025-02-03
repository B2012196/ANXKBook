
namespace UserService.API.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid UserId) : ICommand<DeleteUserResult>;
    public record DeleteUserResult(bool IsSuccess);
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.")
               .NotEqual(Guid.Empty).WithMessage("UserId cannot be an empty GUID.");
        }
    }
    public class DeleteUserHandler(ApplicationDbContext context) : ICommandHandler<DeleteUserCommand, DeleteUserResult>
    {
        public async Task<DeleteUserResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == command.UserId, cancellationToken)
                ?? throw new UserNotFoundException(command.UserId);

            context.Users.Remove(user);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteUserResult(true);
        }
    }
}
