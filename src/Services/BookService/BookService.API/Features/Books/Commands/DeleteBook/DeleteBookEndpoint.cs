namespace BookService.API.Features.Books.Commands.DeleteBook
{
    public record DeleteBookResponse(bool IsSuccess);
    public class DeleteBookEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/books/books/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteBookCommand(id));

                var response = result.Adapt<DeleteBookResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteBook")
            .Produces<DeleteBookResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Book")
            .WithDescription("Delete Book");
        }
    }
}
