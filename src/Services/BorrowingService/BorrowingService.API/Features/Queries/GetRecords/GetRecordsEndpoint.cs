
namespace BorrowingService.API.Features.Queries.GetRecords
{
    public record GetRecordsResponse(IEnumerable<BorrowingRecord> BorrowingRecords);
    public class GetRecordsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/borrowings/records", async (ISender sender) =>
            {
                var result = await sender.Send(new GetRecordsQuery());

                var response = result.Adapt<GetRecordsResponse>();

                return Results.Ok(response);    
            })
            .WithName("GetRecords")
            .Produces<GetRecordsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Records")
            .WithDescription("Get Records");
        }
    }
}
