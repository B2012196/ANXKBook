
namespace BorrowingService.API.Features.Commands.BorrowingBook
{
    public record BorrowingBookRequest(Guid RecordId, List<BorrowedBook> Books);
    public record BorrowingBookResponse(bool IsSuccess);
    public class BorrowingBookEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/borrowings/borrowingbook", async(BorrowingBookRequest request, ISender sender) =>
            {
                var command = request.Adapt<BorrowingBookCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<BorrowingBookResponse>();   

                return Results.Ok(response);
            })
            .WithName("UpdateBorrowingBook")
            .Produces<BorrowingBookResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Borrowing Book")
            .WithDescription("Update Borrowing Book");
        }
    }
}
