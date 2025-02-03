
namespace BookService.API.Features.Books.Repository
{
    public class BookRepository(ApplicationDbContext context) : IBookRepository
    {
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return await context.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task<Guid> CreateBook(Book book, CancellationToken cancellationToken)
        {
            context.Books.Add(book);
            await context.SaveChangesAsync(cancellationToken);

            return book.BookId;
        }

    }
}
