namespace BookService.API.Features.BookStatus.Repository
{
    public interface IStatusRepository
    {
        Task<Status?> GetStatusByNameAsync(string StatusName, CancellationToken cancellationToken);
    }
}
