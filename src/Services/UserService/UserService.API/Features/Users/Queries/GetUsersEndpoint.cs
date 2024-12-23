namespace UserService.API.Features.Users.Queries
{
    public record GetUsersRequest(int? pageNumber = 1, int? pageSize = 10);
    public record GetUsersResponse(IEnumerable<UserDTO> UserDTOs, int totalCount);
    public class GetUsersEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/users/users", async ([FromQuery] int pageNumber, [FromQuery] int pageSize, ISender sender) =>
            {
                var result = await sender.Send(new GetUsersQuery(pageNumber, pageSize));    

                var response = result.Adapt<GetUsersResponse>();

                return Results.Ok(response);
            })
            .WithName("GetUsers")
            .Produces<GetUsersResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Users")
            .WithDescription("Get Users");
        }
    }
}
