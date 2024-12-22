
namespace BookService.API.Features.BookStatus.Queries.GetBookStatus
{
    public record GetBookStatusQuery() : IQuery<GetBookStatusResult>;
    public record GetBookStatusResult(IEnumerable<Status> BookStatuses);
    public class GetBookStatusHandler(ApplicationDbContext context)
        : IQueryHandler<GetBookStatusQuery, GetBookStatusResult>
    {
        public async Task<GetBookStatusResult> Handle(GetBookStatusQuery query, CancellationToken cancellationToken)
        {
            var statuses = await context.BookStatus.ToListAsync(cancellationToken);

            return new GetBookStatusResult(statuses);
        }
    }
}
