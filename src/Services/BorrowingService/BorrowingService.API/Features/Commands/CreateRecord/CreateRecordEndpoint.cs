namespace BorrowingService.API.Features.Commands.CreateRecord
{
    public record CreateRecordRequest
        (string FullName, List<BorrowedBook> Books);
    public record CreateRecordResponse(Guid RecordId);
    public class CreateRecordEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/borrowings/records", async (CreateRecordRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateRecordCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateRecordResponse>();

                return Results.Created($"/borrowings/records/id/{response.RecordId}", response);
            })
            .WithName("CreateRecord")
            .Produces<CreateRecordResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Record")
            .WithDescription("Create Record");
        }
    }
}
