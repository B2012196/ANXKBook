namespace BookService.API.Features.Genres.Commands.UpdateGenre
{
    public record UpdateGenreRequest(Guid GenreId, string GenreName);
    public record UpdateGenreResponse(bool IsSuccess);
    public class UpdateGenreEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/books/genres", async (UpdateGenreRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateGenreCommand>();  

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateGenreResponse>(); 

                return Results.Ok(response);
            })
            .WithName("UpdateGenre")
            .Produces<UpdateGenreResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Genre")
            .WithDescription("Update Genre");
        }
    }
}
