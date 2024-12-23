namespace UserService.API.Features.Roles.Queries.GetRoles
{
    public record GetRolesResponse(IEnumerable<Role> Roles);
    public class GetRolesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/users/roles", async (ISender sender) =>
            {
                var result = await sender.Send(new GetRolesQuery());

                var response = result.Adapt<GetRolesResponse>();   
                
                return Results.Ok(response);
            })
            .WithName("GetRoles")
            .Produces<GetRolesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Roles")
            .WithDescription("Get Roles");
        }
    }
}
