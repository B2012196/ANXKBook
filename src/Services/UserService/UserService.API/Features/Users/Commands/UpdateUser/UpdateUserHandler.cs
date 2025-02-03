
namespace UserService.API.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(Guid UserId, string Email) : ICommand<UpdateUserResult>;
    public record UpdateUserResult(bool IsSuccess);
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.")
                .NotEqual(Guid.Empty).WithMessage("UserId cannot be an empty GUID.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
    public class UpdateUserHandler(ApplicationDbContext context) : ICommandHandler<UpdateUserCommand, UpdateUserResult>
    {
        public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == command.UserId, cancellationToken) 
                ?? throw new UserNotFoundException(command.UserId);

            user.Email = command.Email;

            context.Users.Update(user);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateUserResult(true);
        }
    }
}
