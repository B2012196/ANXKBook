﻿namespace UserService.API.Features.Roles.Commands.UpdateRole
{
    public record UpdateRoleRequest(Guid RoleId, string RoleName);
    public record UpdateRoleResponse(bool IsSuccess);

    public class UpdateRoleEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/users/roles", async (UpdateRoleRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateRoleCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateRoleResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateRole")
            .Produces<UpdateRoleRequest>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Role")
            .WithDescription("Update Role");
        }
    }
}
