namespace BookService.API.Models
{
    public class Book
    {
        public Guid BookId { get; set; }
        public Guid GenreId { get; set; }
        public string Title { get; set; }
        public string? Author { get; set; }
        public string? PublishingHouse { get; set; }
        public int? PublicationYear { get; set; }
        public int Quatity { get; set; }

        [JsonIgnore]
        public Genre Genre { get; set; }

        [JsonIgnore]
        public ICollection<BookCopy> BookCopys { get; set; }

    }
}
