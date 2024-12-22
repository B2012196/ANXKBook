namespace BookService.API.Features.BookStatus.Queries.GetBookStatus
{
    public record GetBookStatusResponse(IEnumerable<Status> BookStatuses);
    public class GetBookStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/books/bookstatus", async (ISender sender) =>
            {
                var result = await sender.Send(new GetBookStatusQuery());

                var response = result.Adapt<GetBookStatusResponse>();   

                return Results.Ok(response);
            })
            .WithName("GetBookStatuses")
            .Produces<GetBookStatusResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create BookStatuses")
            .WithDescription("Create BookStatuses");
        }
    }
}
