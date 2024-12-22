namespace BookService.API.Features.BookStatus.Commands.DeleteBookStatus
{
    public record DeleteStatusResponse(bool IsSuccess);
    public class DeleteStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/books/bookstatus/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteStatusCommand(id));

                var response = result.Adapt<DeleteStatusResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteBookStatus")
            .Produces<DeleteStatusResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete BookStatus")
            .WithDescription("Delete BookStatus");
        }
    }
}
