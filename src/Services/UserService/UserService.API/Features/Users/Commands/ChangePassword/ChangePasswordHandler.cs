
namespace UserService.API.Features.Users.Commands.ChangePassword
{
    public record ChangePasswordCommand(Guid UserId, string Password, string NewPassword) : ICommand<ChangePasswordResult>;
    public record ChangePasswordResult(bool IsSuccess, string Message);
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
