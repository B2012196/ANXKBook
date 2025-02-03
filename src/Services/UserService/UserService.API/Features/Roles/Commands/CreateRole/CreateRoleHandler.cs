namespace UserService.API.Features.Roles.Commands.CreateRole
{
    public record CreateRoleCommand(string RoleName) : ICommand<CreateRoleResult>;
    public record CreateRoleResult(Guid RoleId);
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty().WithMessage("RoleName is required.")
                .MaximumLength(20).WithMessage("RoleName cannot exceed 20 characters.");
        }
    }
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
