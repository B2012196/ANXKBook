namespace BookService.API.Features.Genres.Commands.DeleteGenre
{
    public record DeleteGenreResponse(bool IsSuccess);
    public class DeleteGenreEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/books/genres/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteGenreCommand(id));

                var response = result.Adapt<DeleteGenreResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteGenre")
            .Produces<DeleteGenreResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Genre")
            .WithDescription("Delete Genre");
        }
    }
}
