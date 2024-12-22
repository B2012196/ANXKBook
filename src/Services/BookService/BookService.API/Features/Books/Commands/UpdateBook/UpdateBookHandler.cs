
namespace BookService.API.Features.Books.Commands.UpdateBook
{
    public record UpdateBookCommand(Guid BookId, Guid GenreId, Guid BookStatusId, string Title, string Author,
        string PublishingHouse, int PublicationYear, int Quatity) : ICommand<UpdateBookResult>;
    public record UpdateBookResult(bool IsSuccess);
    public class UpdateBookHandler(ApplicationDbContext context) : ICommandHandler<UpdateBookCommand, UpdateBookResult>
    {
        public async Task<UpdateBookResult> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var book = await context.Books.SingleOrDefaultAsync(b => b.BookId == command.BookId, cancellationToken);
            if(book == null)
            {
                throw new BookNotFoundException(command.BookId);
            }

            book.GenreId = command.GenreId;
            book.BookStatusId = command.BookStatusId;
            book.Title = command.Title;
            book.Author = command.Author;
            book.PublishingHouse = command.PublishingHouse;
            book.PublicationYear = command.PublicationYear;
            book.Quatity = command.Quatity;

            context.Books.Update(book);
            await context.SaveChangesAsync(cancellationToken);

            return new UpdateBookResult(true);
        }
    }
}
