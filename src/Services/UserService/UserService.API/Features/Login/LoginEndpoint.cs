namespace UserService.API.Features.Login
{
    public record LoginRequest(string UserName, string Password);
    public record LoginResponse(bool IsSuccess, TokenResponse TokenResponse);
    public class LoginEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/users/login", async (LoginRequest request, ISender sender) =>
            {
                var command = request.Adapt<LoginCommand>();    

                var result = await sender.Send(command);

                var response = result.Adapt<LoginResponse>();

                if (!response.IsSuccess)
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(response);
            })
            .WithName("Login")
            .Produces<LoginResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .WithSummary("Login")
            .WithDescription("Login");
        }
    }
}
