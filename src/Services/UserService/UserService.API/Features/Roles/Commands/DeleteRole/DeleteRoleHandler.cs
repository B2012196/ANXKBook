
namespace UserService.API.Features.Roles.Commands.DeleteRole
{
    public record DeleteRoleCommand(Guid RoleId) : ICommand<DeleteRoleResult>;
    public record DeleteRoleResult(bool IsSuccess);
    public class DeleteRoleHandler(ApplicationDbContext context) : ICommandHandler<DeleteRoleCommand, DeleteRoleResult>
    {
        public async Task<DeleteRoleResult> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            var role = await context.Roles.SingleOrDefaultAsync(r => r.RoleId == command.RoleId, cancellationToken)
                ?? throw new RoleNotFoundException(command.RoleId);

            context.Roles.Remove(role);
            await context.SaveChangesAsync(cancellationToken);

            return new DeleteRoleResult(true);
        }
    }
}
