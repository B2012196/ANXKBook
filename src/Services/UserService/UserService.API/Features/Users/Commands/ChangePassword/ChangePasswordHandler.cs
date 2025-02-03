
namespace UserService.API.Features.Users.Commands.ChangePassword
{
    public record ChangePasswordCommand(Guid UserId, string Password, string NewPassword) : ICommand<ChangePasswordResult>;
    public record ChangePasswordResult(bool IsSuccess, string Message);
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.")
                .NotEqual(Guid.Empty).WithMessage("UserId cannot be an empty GUID.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(30).WithMessage("Password cannot exceed 30 characters.");

            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("NewPassword is required.")
                .MinimumLength(8).WithMessage("NewPassword must be at least 8 characters long.")
                .MaximumLength(30).WithMessage("NewPassword cannot exceed 30 characters.")
                .Matches(@"[A-Z]").WithMessage("NewPassword must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("NewPassword must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("NewPassword must contain at least one number.")
                .Matches(@"[\W]").WithMessage("NewPassword must contain at least one special character.");
        }
    }
    public class ChangePasswordHandler(ApplicationDbContext context) : ICommandHandler<ChangePasswordCommand, ChangePasswordResult>
    {
        public async Task<ChangePasswordResult> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await context.Users.SingleOrDefaultAsync(u => u.UserId == command.UserId) 
                ?? throw new UserNotFoundException(command.UserId); 

            bool IsSuccess = BCrypt.Net.BCrypt.Verify(command.Password, user.Password);

            if (IsSuccess)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(command.NewPassword);
                context.Users.Update(user);
                await context.SaveChangesAsync(cancellationToken);
                return new ChangePasswordResult(true, "correct");
            }

            return new ChangePasswordResult(false, "The username or password is incorrect");


        }
    }
}
