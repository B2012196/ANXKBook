﻿namespace UserService.API.Features.Roles.Commands.DeleteRole
{
    public record DeleteRoleResponse(bool IsSuccess);
    public class DeleteRoleEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/users/roles/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteRoleCommand(id));

                var response = result.Adapt<DeleteRoleResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteRole")
            .Produces<DeleteRoleResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Role")
            .WithDescription("Delete Role");
        }
    }
}
