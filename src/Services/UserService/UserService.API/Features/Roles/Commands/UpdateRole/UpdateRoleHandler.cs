namespace UserService.API.Features.Roles.Commands.UpdateRole
{
    public record UpdateRoleCommand(Guid RoleId, string RoleName) : ICommand<UpdateRoleResult>;
    public record UpdateRoleResult(bool IsSuccess);
    public class UpdateRoleHandler(ApplicationDbContext context) : ICommandHandler<UpdateRoleCommand, UpdateRoleResult>
    {
        public async Task<UpdateRoleResult> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
        {
            var role = await context.Roles.SingleOrDefaultAsync(r => r.RoleId == command.RoleId, cancellationToken) 
                ?? throw new RoleNotFoundException(command.RoleId);


            role.RoleName = command.RoleName;   

            context.Roles.Update(role);
            await context.SaveChangesAsync();

            return new UpdateRoleResult(true);
        }
    }
}
