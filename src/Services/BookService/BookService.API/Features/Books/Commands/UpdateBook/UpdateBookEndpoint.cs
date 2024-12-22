namespace BookService.API.Features.Books.Commands.UpdateBook
{
    public record UpdateBookRequest(Guid BookId, Guid GenreId, Guid BookStatusId, string Title, string Author,
        string PublishingHouse, int PublicationYear, int Quatity);
    public record UpdateBookResponse(bool IsSuccess);
    public class UpdateBookEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/books/books", async (UpdateBookRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateBookCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateBookResponse>();  

                return Results.Ok(response);
            })
            .WithName("UpdateBook")
            .Produces<UpdateBookResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Book")
            .WithDescription("Update Book");
        }
    }
}
