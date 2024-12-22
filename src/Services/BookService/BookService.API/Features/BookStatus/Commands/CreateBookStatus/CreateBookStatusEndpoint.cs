namespace BookService.API.Features.BookStatus.Commands.CreateBookStatus
{
    public record CreateBookStatusRequest(string StatusName);
    public record CreateBookStatusResponse(Guid StatusId);
    public class CreateBookStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/books/bookstatus", async (CreateBookStatusRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateBookStatusCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateBookStatusResponse>();

                return Results.Created($"/books/bookstatus/id/{response.StatusId}", response);

            })
            .WithName("CreateBookStatus")
            .Produces<CreateBookStatusResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create BookStatus")
            .WithDescription("Create BookStatus");
        }
    }
}
