
namespace BookService.API.Features.BookCopys.Queries.GetBookCopies
{
    public record GetBookCopiesRequest() : IQuery<GetBookCopiesResult>;
    public record GetBookCopiesResponse(IEnumerable<BookCopy> BookCopies);
    public class GetBookCopiesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/books/bookcopy", async (ISender sender) =>
            {
                var result = await sender.Send(new GetBookCopiesQuery());

                var response = result.Adapt<GetBookCopiesResponse>();

                return Results.Ok(response);
            })
            .WithName("GetBookCopies")
            .Produces<GetBookCopiesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Book Copies")
            .WithDescription("Get Book Copies");
        }
    }
}
