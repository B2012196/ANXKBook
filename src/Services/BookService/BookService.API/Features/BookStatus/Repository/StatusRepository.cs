
namespace BookService.API.Features.BookStatus.Repository
{
    public class StatusRepository(ApplicationDbContext context) : IStatusRepository
    {
        public async Task<Status?> GetStatusByNameAsync(string StatusName, CancellationToken cancellationToken)
        {
            return await context.BookStatus.SingleOrDefaultAsync(s => s.StatusName == StatusName, cancellationToken);
        }
    }
}
