namespace BookService.API.Features.BookStatus.Commands.UpdateBookStatus
{
    public record UpdateStatusRequest(Guid StatusId, string StatusName);
    public record UpdateStatusResponse(bool IsSuccess);
    public class UpdateStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/books/bookstatus", async (UpdateStatusRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateStatusCommand>(); 

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateStatusResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateBookStatus")
            .Produces<UpdateStatusResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update BookStatus")
            .WithDescription("Update BookStatus");
        }
    }
}
