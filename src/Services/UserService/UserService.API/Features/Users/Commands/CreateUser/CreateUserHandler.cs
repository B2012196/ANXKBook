namespace UserService.API.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(Guid RoleId, string UserName, string Password, string Email) : ICommand<CreateUserResult>;
    public record CreateUserResult(Guid UserId);
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
