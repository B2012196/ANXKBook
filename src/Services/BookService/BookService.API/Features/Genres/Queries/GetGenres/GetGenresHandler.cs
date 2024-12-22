
namespace BookService.API.Features.Genres.Queries.GetGenres
{
    public record GetGenresQuery() : IQuery<GetGenresResult>;
    public record GetGenresResult(IEnumerable<Genre> Genres);
    public class GetGenresHandler(ApplicationDbContext context)
        : IQueryHandler<GetGenresQuery, GetGenresResult>
    {
        public async Task<GetGenresResult> Handle(GetGenresQuery query, CancellationToken cancellationToken)
        {
            var genres = await context.Genres.ToListAsync(cancellationToken);  
            
            return new GetGenresResult(genres);
        }
    }
}
