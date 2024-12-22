namespace BookService.API.Features.Books.Commands.CreateBook
{
    public record CreateBookCommand(Guid GenreId, Guid BookStatusId, string Title, string? Author,
        string? PublishingHouse, int? PublicationYear, int Quatity) : ICommand<CreateBookResult>;
    public record CreateBookResult(Guid BookId);
    public class CreateBookHandler(ApplicationDbContext context) : ICommandHandler<CreateBookCommand, CreateBookResult>
    {
        public async Task<CreateBookResult> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                BookId = Guid.NewGuid(),
                GenreId = command.GenreId,
                BookStatusId = command.BookStatusId,
                Title = command.Title,
                Author = command.Author,
                PublishingHouse = command.PublishingHouse,
                PublicationYear = command.PublicationYear,
                Quatity = command.Quatity
            };

            context.Books.Add(book);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateBookResult(book.BookId);
        }
    }
}
