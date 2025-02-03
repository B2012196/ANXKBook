namespace BookService.API.Features.Books.Queries.GetBooks
{
    public record GetBooksQuery(int? pageNumber = 1, int? pageSize = 10, Guid filterGenreId = default) : IQuery<GetBooksResult>;
    public record GetBooksResult(IEnumerable<Book> Books, int TotalCount);
    public class GetBooksHandler(ApplicationDbContext context, ILogger<GetBooksHandler> logger) : IQueryHandler<GetBooksQuery, GetBooksResult>
    {
        public async Task<GetBooksResult> Handle(GetBooksQuery query, CancellationToken cancellationToken)
        {
            try
            {
                if (query.filterGenreId == Guid.Empty)
                {
                    throw new GenreNotFoundException(query.filterGenreId);
                }

                var books = context.Books.AsNoTracking()
                    .Where(b => b.GenreId == query.filterGenreId);

                books = books.OrderBy(b => b.Title);

                int totalCount = await books.CountAsync(cancellationToken);

                if (query.pageNumber.HasValue && query.pageSize.HasValue)
                {
                    int skip = (query.pageNumber.Value - 1) * query.pageSize.Value;
                    books = books.Skip(skip).Take(query.pageSize.Value);
                }

                return new GetBooksResult(await books.ToListAsync(cancellationToken), totalCount);
            }
            catch (Exception ex) 
            {
                logger.LogWarning(ex.ToString());
                throw;
            }

        }
    }
}
