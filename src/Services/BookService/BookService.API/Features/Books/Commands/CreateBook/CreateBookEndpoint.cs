namespace BookService.API.Features.Books.Commands.CreateBook
{
    public record CreateBookRequest(Guid GenreId, Guid BookStatusId, string Title, string? Author,
        string? PublishingHouse, int? PublicationYear, int Quatity);
    public record CreateBookResponse(Guid BookId);
    public class CreateBookEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/books/books", async (CreateBookRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateBookCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateBookResponse>();

                return Results.Created($"/books/books/id/{response.BookId}", response);
            })
            .WithName("CreateBook")
            .Produces<CreateBookResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Book")
            .WithDescription("Create Book");
        }
    }
}
