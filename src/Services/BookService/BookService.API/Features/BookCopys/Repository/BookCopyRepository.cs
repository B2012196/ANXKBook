
namespace BookService.API.Features.BookCopys.Repository
{
    public class BookCopyRepository(ApplicationDbContext context) : IBookCopyRepository
    {
        public async Task<bool> UpdateBorrowedStatus(Guid BookCopyId, CancellationToken cancellationToken)
        {
            var bookcopy = await context.BookCopys.SingleOrDefaultAsync(b => b.BookCopyId == BookCopyId, cancellationToken)
                ?? throw new BookNotFoundException(BookCopyId);

            bookcopy.BookStatusId = Guid.Parse("fce486c4-964a-4c4e-ad92-af56d1ee09a5");

            context.BookCopys.Update(bookcopy);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UpdateReturnedStatus(Guid BookCopyId, Guid BookStatusId, CancellationToken cancellationToken)
        {
            var bookcopy = await context.BookCopys.SingleOrDefaultAsync(b => b.BookCopyId == BookCopyId, cancellationToken)
                ?? throw new BookNotFoundException(BookCopyId);

            bookcopy.BookStatusId = BookStatusId;

            context.BookCopys.Update(bookcopy);
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
