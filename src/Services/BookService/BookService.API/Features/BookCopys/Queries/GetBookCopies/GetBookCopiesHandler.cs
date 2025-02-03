
namespace BookService.API.Features.BookCopys.Queries.GetBookCopies
{
    public record GetBookCopiesQuery() : IQuery<GetBookCopiesResult>;
    public record GetBookCopiesResult(IEnumerable<BookCopy> BookCopies);
    public class GetBookCopiesHandler(ApplicationDbContext context) : IQueryHandler<GetBookCopiesQuery, GetBookCopiesResult>
    {
        public async Task<GetBookCopiesResult> Handle(GetBookCopiesQuery request, CancellationToken cancellationToken)
        {
            var bookcopies = await context.BookCopys.ToListAsync(cancellationToken);

            return new GetBookCopiesResult(bookcopies);
        }
    }
}
