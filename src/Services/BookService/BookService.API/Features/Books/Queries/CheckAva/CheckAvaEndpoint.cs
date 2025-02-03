
namespace BookService.API.Features.Books.Queries.CheckAva
{
    public record CheckAvaRequest(Guid BookId);
    public record CheckAvaResponse(bool IsSuccess);
    public class CheckAvaEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/books/books/available/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new CheckAvaQuery(id));

                var response = result.Adapt<CheckAvaResponse>();

                return Results.Ok(response);
            })
            .WithName("Check Available Book")
            .Produces<CheckAvaResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Check Available Book")
            .WithDescription("Check Available Book");
        }
    }
}
