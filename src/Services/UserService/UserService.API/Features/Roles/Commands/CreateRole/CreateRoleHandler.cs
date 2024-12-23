namespace UserService.API.Features.Roles.Commands.CreateRole
{
    public record CreateRoleCommand(string RoleName) : ICommand<CreateRoleResult>;
    public record CreateRoleResult(Guid RoleId);
    public class CreateRoleHandler(ApplicationDbContext context) : ICommandHandler<CreateRoleCommand, CreateRoleResult>
    {
        public async Task<CreateRoleResult> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
        {
            var role = new Role
            {
                RoleId = Guid.NewGuid(),
                RoleName = command.RoleName
            };

            context.Roles.Add(role);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateRoleResult(role.RoleId);
        }
    }
}
