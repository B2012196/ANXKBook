namespace UserService.API.Features.Users.Commands.ChangePassword
{
    public record ChangePasswordRequest(Guid UserId, string Password, string NewPassword);
    public record ChangePasswordResponse(bool IsSuccess, string Message);
    public class ChangePasswordEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/users/users/changepassword", async (ChangePasswordRequest request, ISender sender) =>
            {
                var command = request.Adapt<ChangePasswordCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<ChangePasswordResponse>();

                if(!response.IsSuccess)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(response);
            })
            .WithName("ChangePassword")
            .Produces<ChangePasswordResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithSummary("Change Password")
            .WithDescription("Change Password");
        }
    }
}
