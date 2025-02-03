namespace BookWeb.Models
{
    public class Book
    {
        public Guid BookId { get; set; }
        public Guid GenreId { get; set; }
        public Guid BookStatusId { get; set; }
        public string Title { get; set; }
        public string? Author { get; set; }
        public string? PublishingHouse { get; set; }
        public int? PublicationYear { get; set; }
        public int Quatity { get; set; }
    }

    public record GetBooksResponse(IEnumerable<Book> Books, int TotalCount);
    public record CheckAvaResponse(bool IsSuccess);
}
