namespace UserService.API.Features.Users.Commands.CreateUser
{
    public record CreateUserRequest(Guid RoleId, string UserName, string Password, string Email);
    public record CreateUserResponse(Guid UserId);
    public class CreateUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/users/users", async (CreateUserRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateUserCommand>();   

                var result = await sender.Send(command);

                var response = result.Adapt<CreateUserResponse>();  

                return Results.Created($"/users/roles/id/{response.UserId}", response);
            })
            .WithName("CreateUser")
            .Produces<CreateUserResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create User")
            .WithDescription("Create User");
        }
    }
}
