namespace BorrowingService.API.Features.Commands.DeleteRecord
{
    public record DeleteRecordResponse(bool IsSuccess);
    public class DeleteRecordEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/borrowings/records/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteRecordCommand(id));

                var response = result.Adapt<DeleteRecordResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteRecord")
            .Produces<DeleteRecordResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Record")
            .WithDescription("Delete Record");
        }
    }
}
