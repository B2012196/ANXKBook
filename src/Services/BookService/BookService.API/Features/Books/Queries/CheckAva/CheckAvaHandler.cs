
namespace BookService.API.Features.Books.Queries.CheckAva
{
    public record CheckAvaQuery(Guid BookId) : IQuery<CheckAvaResult>;
    public record CheckAvaResult(bool IsSuccess);
    public class CheckAvaHandler(ApplicationDbContext context) : IQueryHandler<CheckAvaQuery, CheckAvaResult>
    {
        public async Task<CheckAvaResult> Handle(CheckAvaQuery query, CancellationToken cancellationToken)
        {
            Guid StatusAva = Guid.Parse("76aa4a74-88ee-46c9-944b-98f644e40ff1");

            var book = await context.Books.SingleOrDefaultAsync(b => b.BookId == query.BookId, cancellationToken)
                ?? throw new BookNotFoundException(query.BookId);

            return new CheckAvaResult(true);
        }
    }
}
