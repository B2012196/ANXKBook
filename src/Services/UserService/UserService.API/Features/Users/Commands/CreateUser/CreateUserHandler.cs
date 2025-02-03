namespace UserService.API.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(Guid RoleId, string UserName, string Password, string Email) : ICommand<CreateUserResult>;
    public record CreateUserResult(Guid UserId);
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.RoleId).NotEmpty().WithMessage("RoleId is required.")
                .NotEqual(Guid.Empty).WithMessage("RoleId cannot be an empty GUID.");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.")
                .MinimumLength(5).WithMessage("UserName must be at least 5 characters long.")
                .MaximumLength(20).WithMessage("UserName cannot exceed 20 characters.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .MaximumLength(30).WithMessage("Password cannot exceed 30 characters.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W]").WithMessage("Password must contain at least one special character.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
    public class CreateUserHandler(ApplicationDbContext context) : ICommandHandler<CreateUserCommand, CreateUserResult>
    {
        public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                RoleId = command.RoleId,
                UserName = command.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(command.Password),
                Email = command.Email
            };

            context.Users.Add(user);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateUserResult(user.UserId);
        }
    }
}
