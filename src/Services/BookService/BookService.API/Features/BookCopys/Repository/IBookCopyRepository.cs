namespace BookService.API.Features.BookCopys.Repository
{
    public interface IBookCopyRepository
    {
        Task<bool> UpdateBorrowedStatus(Guid BookCopyId, CancellationToken cancellationToken);
        Task<bool> UpdateReturnedStatus(Guid BookCopyId, Guid BookStatusId, CancellationToken cancellationToken);

    }
}
