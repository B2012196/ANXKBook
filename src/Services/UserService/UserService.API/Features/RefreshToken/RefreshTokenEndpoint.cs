namespace UserService.API.Features.RefreshToken
{
    public record RefreshTokenRequest(string RefreshToken);
    public record RefreshTokenResponse(TokenResponse TokenResponse);
    public class RefreshTokenEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/users/refresh", async (RefreshTokenRequest request, ISender sender ) =>
            {
                var result = await sender.Send(new RefreshTokenCommand(request.RefreshToken));

                var response = result.Adapt<RefreshTokenResponse>();    

                return Results.Ok(response);
            })
            .WithName("RefreshToken")
            .Produces<RefreshTokenResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("RefreshToken")
            .WithDescription("RefreshToken");
        }
    }
}
