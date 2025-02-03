namespace BookService.API.Features.BookCopys.Commands.CreateBookCopy
{
    public record CreateBookCopyRequest(Guid BookId);
    public record CreateBookCopyResponse(Guid BookCopyId);
    public class CreateBookCopyEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/books/bookcopy", async (CreateBookCopyRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateBookCopyCommand>();
                
                var result = await sender.Send(command);

                var response = result.Adapt<CreateBookCopyResponse>();

                return Results.Created($"/books/bookcopy/id/{response.BookCopyId}", response);
            })
            .WithName("CreateBookCopy")
            .Produces<CreateBookCopyResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Book Copy")
            .WithDescription("Create Book Copy");
        }
    }
}
