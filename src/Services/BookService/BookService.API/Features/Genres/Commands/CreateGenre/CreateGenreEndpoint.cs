namespace BookService.API.Features.Genres.Commands.CreateGenre
{
    public record CreateGenreRequest(string GenreName);
    public record CreateGenreResponse(Guid GenreId);
    public class CreateGenreEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/books/genres", async (CreateGenreRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateGenreCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateGenreResponse>();

                return Results.Created($"/books/genres/id/{response.GenreId}", response);
            })
            .WithName("CreateGenre")
            .Produces<CreateGenreResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Genre")
            .WithDescription("Create Genre");
        }
    }
}
