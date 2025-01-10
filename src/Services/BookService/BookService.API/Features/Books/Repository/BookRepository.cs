
namespace BookService.API.Features.Books.Repository
{
    public class BookRepository(ApplicationDbContext context) : IBookRepository
    {
        public async Task<bool> UpdateBorrowedStatus(Guid BookId, CancellationToken cancellationToken)
        {
            var book = await context.Books.SingleOrDefaultAsync(b => b.BookId == BookId, cancellationToken)
                ?? throw new BookNotFoundException(BookId);

            book.BookStatusId = Guid.Parse("fce486c4-964a-4c4e-ad92-af56d1ee09a5");

            context.Books.Update(book);
            await context.SaveChangesAsync(cancellationToken);
            return true;    
        }

        public async Task<bool> UpdateReturnedStatus(Guid BookId, Guid BookStatusId, CancellationToken cancellationToken)
        {
            var book = await context.Books.SingleOrDefaultAsync(b => b.BookId == BookId, cancellationToken)
                ?? throw new BookNotFoundException(BookId);

            book.BookStatusId = BookStatusId;

            context.Books.Update(book);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
