namespace BookWeb.Models
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string GenreName { get; set; }
    }

    public record GetGenresResponse(IEnumerable<Genre> Genres);
}
