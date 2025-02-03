namespace BookService.API.Features.Books.Repository
{
    public interface IBookRepository
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
        Task<Guid> CreateBook(Book book, CancellationToken cancellationToken);
    }
}
