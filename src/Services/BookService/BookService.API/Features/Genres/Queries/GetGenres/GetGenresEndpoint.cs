namespace BookService.API.Features.Genres.Queries.GetGenres
{
    public record GetGenresResponse(IEnumerable<Genre> Genres);
    public class GetGenresEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/books/genres", async (ISender sender) =>
            {
                var result = await sender.Send(new GetGenresQuery());

                var response = result.Adapt<GetGenresResponse>();  
                
                return Results.Ok(response);
            })
            .WithName("GetGenres")
            .Produces<GetGenresResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Genres")
            .WithDescription("Get Genres");
        }
    }
}
