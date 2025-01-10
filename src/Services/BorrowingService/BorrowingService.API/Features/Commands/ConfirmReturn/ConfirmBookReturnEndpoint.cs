namespace BorrowingService.API.Features.Commands.ConfirmReturn
{
    public record ConfirmBookReturnRequest(Guid RecordId, List<ReturnedBook> Books);
    public record ConfirmBookReturnResponse(bool IsSuccess);
    public class ConfirmBookReturnEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/borrowings/returnbook", async (ConfirmBookReturnRequest request, ISender sender) =>
            {
                var command = request.Adapt<ConfirmBookReturnCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<ConfirmBookReturnResponse>();
                return Results.Ok(response);
            })
            .WithName("UpdateReturnBook")
            .Produces<ConfirmBookReturnResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Return Book")
            .WithDescription("Update Return Book");
        }
    }
}
