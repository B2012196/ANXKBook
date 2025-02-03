namespace BookService.API.Features.Books.Queries.GetBooks
{
    public record GetBooksRequest(int? pageNumber = 1, int? pageSize = 10, Guid filterGenreId = default);
    public record GetBooksResponse(IEnumerable<Book> Books, int TotalCount);
    public class GetBooksEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/books/books", async ([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] Guid filterGenreId, ISender sender) =>
            {
                var result = await sender.Send(new GetBooksQuery(pageNumber, pageSize, filterGenreId));

                var response = result.Adapt<GetBooksResponse>();

                return Results.Ok(response);
            })
            .WithName("GetBooks")
            .Produces<GetBooksResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Books")
            .WithDescription("Get Books");
        }
    }
}
