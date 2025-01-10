namespace BookService.API.Features.Books.Repository
{
    public interface IBookRepository
    {
        Task<bool> UpdateBorrowedStatus(Guid BookId, CancellationToken cancellationToken);
        Task<bool> UpdateReturnedStatus(Guid BookId, Guid BookStatusId, CancellationToken cancellationToken);
    }
}
