namespace UserService.API.Features.Users.Commands.DeleteUser
{
    public record DeleteUserResponse(bool IsSuccess);
    public class DeleteUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/users/users/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteUserCommand(id));

                var response = result.Adapt<DeleteUserResponse>();  

                return Results.Ok(response);    
            })
            .WithName("DeleteUser")
            .Produces<DeleteUserResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete User")
            .WithDescription("Delete User");
        }
    }
}
