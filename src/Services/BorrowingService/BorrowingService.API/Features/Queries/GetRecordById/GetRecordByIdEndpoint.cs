namespace BorrowingService.API.Features.Queries.GetRecordById
{
    public record GetRecordByIdResponse(BorrowingRecord BorrowingRecord);
    public class GetRecordByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/borrowings/records/id/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetRecordByIdQuery(id));

                var response = result.Adapt<GetRecordByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetRecordById")
            .Produces<GetRecordByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Record By Id")
            .WithDescription("Get Record By Id");
        }
    }
}
